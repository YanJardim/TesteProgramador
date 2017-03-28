using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed, fireRatio;
    public bool dragging, canShoot;

    public GameObject bullet;

    private Transform Gun { get; set; }

    // Use this for initialization
    void Start()
    {
        Gun = transform.FindChild("Gun");
        rb = GetComponent<Rigidbody2D>();
        canShoot = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Shoot();
    }

    //TODO: Implementar movimentação por touch TAMBÉM
    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        rb.MovePosition((Vector2)transform.position + (new Vector2(h, v) * Time.deltaTime * speed));
    }

    private void Shoot()
    {
        if (Input.GetAxis("Fire1") > 0 && canShoot)
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
