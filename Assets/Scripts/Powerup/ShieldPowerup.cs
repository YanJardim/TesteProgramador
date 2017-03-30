using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerup : Powerup
{
    //Prefab de efeito do shield
    public GameObject shieldEffect;
    public override void Effect()
    {
        GameObject shield = Instantiate(shieldEffect, GameManager.Instance.player.gameObject.transform.position, shieldEffect.transform.rotation);
        shield.transform.SetParent(GameManager.Instance.player.transform);
    }

    // Use this for initialization
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
