using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PosGameScoreUI : MonoBehaviour
{

    public Text txtName;
    public Text txtScore;
    public GameObject rankingCavas;


    private SerializableScore score;

    // Use this for initialization
    void Start()
    {
        //Cria um novo SerializableScore
        score = new SerializableScore();
        //Pega o score da sessão atual
        score.Score = GameManager.Instance.score;
        //Atualiza o testo de score do painel de score
        txtScore.text = "Score: " + GameManager.Instance.score;

    }

    /// <summary>
    /// Metodo para chamar o painel de ranking
    /// </summary>
    public void CallNextUI()
    {

        score.Name = txtName.text;
        AddScoreToRanking();

        Instantiate(rankingCavas);
        Destroy(gameObject);

    }
    /// <summary>
    /// Metodo para adicionar o score da sessão atual para o arquivo rankings.sr
    /// </summary>
    private void AddScoreToRanking()
    {
        RankingUtils.AddRanking(score);

    }
}
