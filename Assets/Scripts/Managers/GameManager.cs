using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    private const string constScoreText = "Score: ";

    public int Score { get; set; }
    public Text scoreText;
    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = constScoreText + Score;
    }

    public void AddScore(int amount)
    {
        Score += amount;
    }


}
