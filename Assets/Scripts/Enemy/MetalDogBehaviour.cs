using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class MetalDogBehaviour : Enemy
{
    public override void OnAttack()
    {
        Rb.MovePosition((Vector2)transform.position + new Vector2(-speed * Time.deltaTime, 0));
    }

    public override void OnIdle()
    {
        Rb.MovePosition((Vector2)transform.position + new Vector2(-speed * Time.deltaTime, 0));
    }

    public override void OnTrigger()
    {
        Rb.MovePosition((Vector2)transform.position + new Vector2(-speed * Time.deltaTime, 0));
    }

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

