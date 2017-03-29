using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GAMESTATES { GAME, PAUSE, RANK };

public class GameManager : Singleton<GameManager>
{

    //String constante do texto de Score
    private const string constScoreText = "Score: ";

    //Variavel para guardar o score da sessão atual
    public int score;

    public GAMESTATES currentGameState;

    //Referencia para o objeto UI de texto na cena
    public Text scoreText;
    public GameObject scoreCanvas;
    // Use this for initialization
    void Start()
    {
        currentGameState = GAMESTATES.GAME;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentGameState)
        {
            case GAMESTATES.GAME:
                scoreText.text = constScoreText + score;
                break;
            case GAMESTATES.PAUSE:

                break;
            case GAMESTATES.RANK:
                Time.timeScale = 0;

                break;
        }

    }

    /// <summary>
    /// Metodo para adicionar score
    /// </summary>
    /// <param name="amount"></param>
    public void AddScore(int amount)
    {
        score += amount;
    }

    public bool IsCurrentGameState(GAMESTATES state)
    {
        return currentGameState == state;
    }
    public void SetCurrentGameState(GAMESTATES state)
    {
        currentGameState = state;
    }

    public void SetStateToRank()
    {
        SetCurrentGameState(GAMESTATES.RANK);
        Instantiate(scoreCanvas);
    }



}
