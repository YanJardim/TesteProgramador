using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PosGameScoreUI : MonoBehaviour
{

    public Text txtName;
    public Text txtScore;
    public GameObject rankingCavas;

    public List<GameObject> elementsToHide;

    private SerializableScore score;

    // Use this for initialization
    void Start()
    {

        /*elementsToHide.Add();
        elementsToHide.Add(transform.FindChild("HeaderPanel").FindChild("txtFieldName").gameObject);*/

        score = new SerializableScore();
        score.Score = GameManager.Instance.score;
        txtScore.text = "Score: " + GameManager.Instance.score;
        /*if (RankingUtils.EligibleToTop5(score.Score))
        {
            //for (int i = 0; i < elementsToHide.Count; i++)
            //{
            transform.FindChild("HeaderPanel").FindChild("txtName").gameObject.SetActive(false);
            transform.FindChild("HeaderPanel").FindChild("txtFieldName").gameObject.SetActive(false);
            //elementsToHide[i].SetActive(false);
            //}
        }*/
    }


    public void CallNextUI()
    {
        //if (txtName.text.Length > 0)
        //{
        score.Name = txtName.text;
        AddScoreToRanking();

        Instantiate(rankingCavas);
        Destroy(gameObject);
        //}
    }

    private void AddScoreToRanking()
    {
        RankingUtils.AddRanking(score);

    }
}
