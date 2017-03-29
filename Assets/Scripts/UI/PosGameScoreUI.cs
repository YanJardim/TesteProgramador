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
        score = new SerializableScore();
        score.Score = GameManager.Instance.score;
        txtScore.text = "Score: " + GameManager.Instance.score;

    }


    public void CallNextUI()
    {
        if (txtName.text.Length > 0)
        {
            score.Name = txtName.text;
            AddScoreToRanking();
            Instantiate(rankingCavas);
            Destroy(gameObject);
        }
    }

    private void AddScoreToRanking()
    {
        Ranking.AddRanking(score);

    }
}
