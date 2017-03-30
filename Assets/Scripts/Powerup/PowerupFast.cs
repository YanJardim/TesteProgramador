using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupFast : Powerup
{
    public override void Effect()
    {
        if (!GameManager.Instance.player.powerupFast)
        {
            GameManager.Instance.player.ChangePowerup();
            GameManager.Instance.player.ActiveFastBullet();
            GameManager.Instance.player.powerupFast = true;
        }
        else GameManager.Instance.player.powerupTimerMax += 10;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
