using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe para controlar o comportamento do Metal Shark
/// </summary>
public class MetalSharkBehaviour : Enemy
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

    // Use this for initialization
    void Start()
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
