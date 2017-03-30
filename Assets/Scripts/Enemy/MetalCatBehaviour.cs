using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe para controlar o comportamento do Metal Cat
/// </summary>
public class MetalCatBehaviour : Enemy
{
    //Referencia para a prefab de bala
    public GameObject bullet;
    //Variavel para dizer de quanto em quanto tempo o inimigo atira
    public float fireRatio;
    //Tempo atual
    private float timer;

    /// <summary>
    /// Metodo para executar o estado de Attack
    /// </summary>
    public override void OnAttack()
    {
        OnIdle();

    }
    /// <summary>
    /// Metodo para executar o estado de Idle
    /// </summary>
    public override void OnIdle()
    {
        //Move o inimigo para esquerda
        Rb.MovePosition((Vector2)transform.position + new Vector2(-speed * Time.deltaTime, 0));
        //Conta o tempo
        timer += Time.deltaTime;
        //Verifica se o tempo atual é maior que o fireRatio
        if (timer >= fireRatio)
        {
            //Pega a posição do player
            Vector2 playerPos = Player.transform.position;
            //Muda a direção da bala para ser a posição do player
            bullet.GetComponent<BulletBehaviour>().direction = (playerPos - (Vector2)transform.position).normalized;
            //Instancia a bala
            Instantiate(bullet, transform.position, bullet.transform.rotation);
            //Seta o tempo para 0
            timer = 0;

        }

    }
    /// <summary>
    /// Metodo para executar o estado de Trigger
    /// </summary>
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
