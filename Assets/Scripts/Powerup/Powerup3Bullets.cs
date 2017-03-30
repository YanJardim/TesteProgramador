using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup3Bullets : Powerup
{
    public override void Effect()
    {

        if (!GameManager.Instance.player.powerup3Bullets)
        {
            GameManager.Instance.player.ChangePowerup();
            GameManager.Instance.player.powerup3Bullets = true;
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
