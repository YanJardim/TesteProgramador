using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Classe para controlar o comportamento do Metal Dog
/// </summary>
class MetalDogBehaviour : Enemy
{

    public override void OnAttack()
    {
        //Move o inimigo para esquerda
        Rb.MovePosition((Vector2)transform.position + new Vector2(-speed * Time.deltaTime, 0));
    }

    public override void OnIdle()
    {
        //Move o inimigo para esquerda
        Rb.MovePosition((Vector2)transform.position + new Vector2(-speed * Time.deltaTime, 0));
    }

    public override void OnTrigger()
    {
        //Move o inimigo para esquerda
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

