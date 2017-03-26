using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public int Score { get; set; }

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore(int amount)
    {
        Score += amount;
    }


}
