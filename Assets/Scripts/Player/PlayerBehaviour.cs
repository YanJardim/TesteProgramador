using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    private int invTimesMax, invTimes;

    private Rigidbody2D rb;
    private Transform Gun { get; set; }
    private Bounds backgroundBounds, playerBounds;
    private Vector2 playerSize;

    public float speed, fireRatio, invulnerabilityTime;
    public bool dragging, canShoot, invulnerability;
    public int maxHp, hp;

    public GameObject bullet, background;
    public Text hpText;





    // Use this for initialization
    void Start()
    {
        Gun = transform.FindChild("Gun");
        rb = GetComponent<Rigidbody2D>();
        canShoot = true;
        hp = maxHp;
        invTimesMax = 3;
        invTimes = 0;
        invulnerability = false;

        backgroundBounds = background.GetComponent<SpriteRenderer>().bounds;
        playerBounds = GetComponent<SpriteRenderer>().bounds;
        playerSize = new Vector2(playerBounds.max.x - playerBounds.min.x, playerBounds.max.y - playerBounds.min.y);

    }

    private void Update()
    {
        if (GameManager.Instance.IsCurrentGameState(GAMESTATES.GAME))
        {
            if (IsDead())
            {
                GameManager.Instance.SetStateToRank();
                SoundManager.Instance.PlaySfx("player_Explosion");
            }
            LimitPlayerOnBackground();
            Shoot();
            hpText.text = "x " + hp;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (GameManager.Instance.IsCurrentGameState(GAMESTATES.GAME))
        {
            Move();
            TouchMove();
        }

    }

    public void SubHp(int amount)
    {
        hp -= amount;
    }

    private void Move()
    {
        //TODO: Verificar se tem algum joystick conectado antes de movimentar
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
                //TODO: Mudar essa linha para funcionar com rigidbody
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
            aux.GetComponent<BulletBehaviour>().direction = Vector2.right;
            StartCoroutine(FireRatioCooldown());
            SoundManager.Instance.PlaySfx("tiro2");
        }
    }

    private IEnumerator FireRatioCooldown()
    {
        canShoot = false;

        yield return new WaitForSeconds(fireRatio);

        canShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "EnemyBullet" && !invulnerability && GameManager.Instance.IsCurrentGameState(GAMESTATES.GAME))
        {
            SubHp(1);
            if (hp > 0) SoundManager.Instance.PlaySfx("player_Hit");
            invulnerability = true;
            StartCoroutine(InvulnerabilityCoroutine());

        }
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        Color oldColor = Color.white;
        Color newColor = oldColor;

        if (invTimes <= invTimesMax)
        {


            newColor.a = 0;
            renderer.color = newColor;

            yield return new WaitForSeconds(invulnerabilityTime / 2);

            renderer.color = oldColor;

            yield return new WaitForSeconds(invulnerabilityTime / 2);
            invTimes++;
            StartCoroutine(InvulnerabilityCoroutine());

        }
        else
        {
            renderer.color = oldColor;
            invTimes = 0;
            invulnerability = false;
        }

    }
    public bool IsDead()
    {
        return hp <= 0;
    }
    public void LimitPlayerOnBackground()
    {
        Vector2 playerPos = transform.position;

        playerPos.x = Mathf.Clamp(playerPos.x, backgroundBounds.min.x + playerSize.x / 2, backgroundBounds.max.x - playerSize.x / 2);
        playerPos.y = Mathf.Clamp(playerPos.y, backgroundBounds.min.y + playerSize.y / 2, backgroundBounds.max.y - playerSize.y / 2);

        transform.position = playerPos;

    }
}
