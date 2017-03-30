using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe para gerenciar o painel de ranking
/// </summary>
public class RankingPanel : MonoBehaviour
{
    //Referencia para o painel de score na cena
    public GameObject scorePanel;

    // Use this for initialization
    void Start()
    {
        LoadRankings();
    }

    /// <summary>
    /// Metodo para carregar os rankings e mostrar no painel
    /// </summary>
    public void LoadRankings()
    {
        //Carrega os rankings atuais do arquivo rankings.sr em uma lista
        List<SerializableScore> scoresList = RankingUtils.GetRankings();
        //Limpa todos os objetos de score no painel de ranking
        ClearChilds();
        //Conta quantos elementos tem na lista
        int scoreListCount = scoresList.Count;
        //Caso seja maior que 5 mudar o valor para 5
        scoreListCount = scoreListCount > 5 ? 5 : scoreListCount;
        //Verifica se a lista tem mais que 0 elementos
        if (scoreListCount > 0)
        {
            //Percorre a lista
            for (int i = 0; i < scoreListCount; i++)
            {
                //Seta o painel de score com as informações do score atual
                scorePanel.transform.GetChild(0).GetComponent<Text>().text = scoresList[i].Name;
                scorePanel.transform.GetChild(1).GetComponent<Text>().text = scoresList[i].Score.ToString();
                scorePanel.transform.GetChild(2).GetComponent<Text>().text = (i + 1).ToString();
                //Instancia o painel de score no painel de ranking
                GameObject newScore = Instantiate(scorePanel);
                //Muda o nome do painel de score
                newScore.name = scoresList[i].Name + " ScorePanel";
                //Seta o novo painel de score como filho do painel de ranking
                newScore.transform.SetParent(transform, false);
            }
        }


    }
    /// <summary>
    /// Metodo para limpar todos os objetos de score
    /// </summary>
    public void ClearChilds()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
    /// <summary>
    /// Metodo para voltar a cena de Menu
    /// </summary>
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    /// <summary>
    /// Metodo para resetar o jogo
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
