using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GAMESTATES { GAME, PAUSE, RANK };

public class GameManager : Singleton<GameManager>
{
    //Referencia para o player
    public PlayerBehaviour player;

    //String constante do texto de Score
    private const string constScoreText = "Score: ";

    //Variavel para guardar o score da sessão atual
    public int score;

    public GAMESTATES currentGameState;

    //Referencia para o texto de score na cena
    public Text scoreText;
    //Referencia para a prefab canvas de score
    public GameObject scoreCanvas, background;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        currentGameState = GAMESTATES.GAME;
        score = 0;
        SoundManager.Instance.PlaySfx("Fx1");
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentGameState)
        {
            case GAMESTATES.GAME:
                Time.timeScale = 1;
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
    /// <summary>
    /// Metodo para verificar o estado atual do jogo
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public bool IsCurrentGameState(GAMESTATES state)
    {
        return currentGameState == state;
    }

    /// <summary>
    /// Metodo para setar o estado atual de jogo
    /// </summary>
    /// <param name="state"></param>
    public void SetCurrentGameState(GAMESTATES state)
    {
        currentGameState = state;
    }

    /// <summary>
    /// Metodo para setar o estado atual para RANK
    /// </summary>
    public void SetStateToRank()
    {
        SetCurrentGameState(GAMESTATES.RANK);

        //Instancia o canvas de score na cena
        Instantiate(scoreCanvas);
    }
}
