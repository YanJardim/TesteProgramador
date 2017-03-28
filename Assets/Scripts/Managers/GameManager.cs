using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    //String constante do texto de Score
    private const string constScoreText = "Score: ";

    //Variavel para guardar o score da sessão atual
    public int Score { get; set; }



    //Referencia para o objeto UI de texto na cena
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

    /// <summary>
    /// Metodo para adicionar score
    /// </summary>
    /// <param name="amount"></param>
    public void AddScore(int amount)
    {
        Score += amount;
    }




}
