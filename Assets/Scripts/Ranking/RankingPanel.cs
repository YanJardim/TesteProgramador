using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingPanel : MonoBehaviour
{

    public GameObject scorePanel;

    // Use this for initialization
    void Start()
    {
        /*List<SerializableScore> scoresList = new List<SerializableScore>();
        SerializableScore s1 = new SerializableScore("Maria", 550);
        SerializableScore s2 = new SerializableScore("Jose", 20);
        SerializableScore s3 = new SerializableScore("João", 50);
        SerializableScore s4 = new SerializableScore("Jorge", 300);
        SerializableScore s5 = new SerializableScore("Tavares", 220);
        SerializableScore s6 = new SerializableScore("Juan", 30);
        SerializableScore s7 = new SerializableScore("Mario", 450);
        SerializableScore s8 = new SerializableScore("Paula", 70);
        SerializableScore s9 = new SerializableScore("Gerogia", 80);
        SerializableScore s10 = new SerializableScore("Joana", 720);

        scoresList.Add(s1);
        scoresList.Add(s2);
        scoresList.Add(s3);
        scoresList.Add(s4);
        scoresList.Add(s5);
        scoresList.Add(s6);
        scoresList.Add(s7);
        scoresList.Add(s8);
        scoresList.Add(s9);
        scoresList.Add(s10);


        Ranking.SaveRankings(scoresList);*/


        LoadRankings();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadRankings()
    {
        List<SerializableScore> scoresList = Ranking.GetRankings();
        ClearChilds();

        int scoreListCount = scoresList.Count;
        scoreListCount = scoreListCount > 5 ? 5 : scoreListCount;

        if (scoreListCount > 0)
        {
            for (int i = 0; i < scoreListCount; i++)
            {
                scorePanel.transform.GetChild(0).GetComponent<Text>().text = scoresList[i].Name;
                scorePanel.transform.GetChild(1).GetComponent<Text>().text = scoresList[i].Score.ToString();
                scorePanel.transform.GetChild(2).GetComponent<Text>().text = (i + 1).ToString();

                GameObject newScore = Instantiate(scorePanel);

                newScore.name = scoresList[i].Name + " ScorePanel";
                newScore.transform.SetParent(transform, false);
            }
        }
        //}

    }

    public void ClearChilds()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
