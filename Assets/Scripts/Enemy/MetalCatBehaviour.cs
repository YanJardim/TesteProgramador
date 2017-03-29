using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalCatBehaviour : Enemy
{

    public GameObject bullet;
    public float fireRatio;

    private float timer;

    public override void OnAttack()
    {
        /*Vector3 movePosition = transform.position;
        movePosition = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
        Rb.MovePosition(movePosition);*/
        OnIdle();

    }

    public override void OnIdle()
    {
        Rb.MovePosition((Vector2)transform.position + new Vector2(-speed * Time.deltaTime, 0));
        timer += Time.deltaTime;

        if (timer >= fireRatio)
        {
            Vector2 playerPos = Player.transform.position;
            bullet.GetComponent<BulletBehaviour>().direction = (playerPos - (Vector2)transform.position).normalized;

            Instantiate(bullet, transform.position, bullet.transform.rotation);
            timer = 0;

        }


    }

    public override void OnTrigger()
    {
        OnIdle();
    }

    // Use this for initialization
    private void Start()
    {
        base.Start();
    }

    private void Update()
    {
        DestroyEnemy();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        ExecuteFSM();
    }
}
