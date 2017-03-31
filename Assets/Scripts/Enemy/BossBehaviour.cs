using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : Enemy
{

    public float changeDirectionMaxTime, fireRatio;
    public GameObject bullet;

    public float changeDirectionTimer, shootTimer;
    private Vector2 direction;
    public bool canChangeState, onCenterPoint;
    private Vector2 standardPosition;
    private Bounds backgroundBounds;

    private Vector2 centerPoint;

    public override void OnAttack()
    {
        Debug.Log("ONATTACK");
        if (Vector2.Distance((Vector2)transform.position, centerPoint) > 1 && !onCenterPoint)
        {

            Vector2 dir = (centerPoint - (Vector2)transform.position).normalized;
            Rb.MovePosition((Vector2)transform.position + dir * speed * Time.deltaTime);
        }
        else
        {
            canChangeState = true;
            changeDirectionTimer += Time.deltaTime;
            onCenterPoint = true;
            MoveUpDown();
            shootTimer += Time.deltaTime;

            int rand = Random.Range(0, 100);
            if (rand < 30 && shootTimer >= fireRatio)
            {
                Vector2 playerPos = Player.transform.position;
                Shoot((Vector2)transform.position - playerPos);
            }
            else if (rand >= 30 && shootTimer >= fireRatio) shootTimer = 0;

        }
    }

    public override void OnIdle()
    {


        //if (!ScreenUtils.IsInside(new Vector2(transform.position.x + size.x, transform.position.y), backgroundBounds))
        if (Vector2.Distance((Vector2)transform.position, centerPoint) > 1 && !onCenterPoint)
        {

            Vector2 dir = (centerPoint - (Vector2)transform.position).normalized;
            Rb.MovePosition((Vector2)transform.position + dir * speed * Time.deltaTime);
        }
        else
        {
            canChangeState = true;
            changeDirectionTimer += Time.deltaTime;
            onCenterPoint = true;
            MoveUpDown();
            shootTimer += Time.deltaTime;

            int rand = Random.Range(0, 100);
            if (rand < 30 && shootTimer >= fireRatio)
            {
                Shoot(Vector2.left);
            }
            else if (rand >= 30 && shootTimer >= fireRatio) shootTimer = 0;

        }
    }

    public override void OnTrigger()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= fireRatio)
        {
            Shoot(Vector2.left);
        }

    }

    // Use this for initialization
    void Start()
    {
        base.Start();

        SpawnManager.Instance.canSpawn = false;

        hp = (int)Mathf.Pow(SpawnManager.Instance.spawnLevel, 2) + 10;
        backgroundBounds = GameManager.Instance.background.GetComponent<SpriteRenderer>().bounds;

        shootTimer = 0;
        changeDirectionTimer = 0;
        direction = Vector2.up;
        canChangeState = false;
        centerPoint = new Vector2(3, 0);
        onCenterPoint = false;
    }

    // Update is called once per frame
    void Update()
    {
        LimitEnemy();
        ExecuteFSM();

    }

    void LimitEnemy()
    {
        Vector2 pos = transform.position;

        pos.y = Mathf.Clamp(pos.y, backgroundBounds.min.y + size.y / 2, backgroundBounds.max.y - size.y / 2);
        transform.position = pos;
    }

    public override void ChangeState(STATE newState)
    {
        if (canChangeState)
            CurrentState = newState;
    }
    public void Shoot(Vector2 dir)
    {

        //Pega a posição do player

        //Muda a direção da bala para ser a posição do player

        bullet.GetComponent<BulletBehaviour>().direction = dir;
        //Instancia a bala
        Instantiate(bullet, transform.position, bullet.transform.rotation);
        //Seta o tempo para 0
        shootTimer = 0;


    }

    public void MoveUpDown()
    {

        if (changeDirectionTimer > changeDirectionMaxTime)
        {
            if (direction == Vector2.up) direction = Vector2.down;
            else if (direction == Vector2.down) direction = Vector2.up;

            changeDirectionTimer = 0;
        }
        Rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    public override void WhenDie()
    {
        SpawnManager.Instance.canSpawn = true;
    }
}
