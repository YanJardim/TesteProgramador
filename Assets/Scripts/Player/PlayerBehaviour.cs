using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed, fireRatio;
    public bool dragging, canShoot;

    public GameObject bullet;

    private Transform Gun { get; set; }

    public int maxHp, hp;

    public Text hpText;


    // Use this for initialization
    void Start()
    {
        Gun = transform.FindChild("Gun");
        rb = GetComponent<Rigidbody2D>();
        canShoot = true;
        hp = maxHp;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        TouchMove();
        Shoot();

        hpText.text = "x " + hp;

    }

    public void SubHp(int amount)
    {
        hp -= amount;
    }



    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        rb.MovePosition((Vector2)transform.position + (new Vector2(h, v) * Time.deltaTime * speed));
    }

    private void TouchMove()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {

                if (Physics2D.Raycast(touchPos, touchPos, 0.01f, 1 << LayerMask.NameToLayer("Player")))
                {
                    dragging = true;
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                dragging = false;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved && dragging)
            {
                transform.position = touchPos;
            }


        }
    }

    private void Shoot()
    {
        //TODO: só atirar se o dragging estiver true e se não estiver conectado nenhum controle
        if (Input.GetAxis("Fire1") != 0 && canShoot)
        {
            GameObject aux = Instantiate(bullet, Gun.position, bullet.transform.rotation);
            aux.GetComponent<BulletBehaviour>().direction = false;
            StartCoroutine(FireRatioCooldown());
        }
    }

    private IEnumerator FireRatioCooldown()
    {
        canShoot = false;

        yield return new WaitForSeconds(fireRatio);

        canShoot = true;
    }
}
