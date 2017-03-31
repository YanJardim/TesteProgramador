using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{

    public GameObject settingsPanel, areYouSurePanel;

    /// <summary>
    /// Metodo para carregar a cena Game
    /// </summary>
    public void Play()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
    }

    public void AreYouSure()
    {
        settingsPanel.SetActive(false);
        areYouSurePanel.SetActive(true);
    }

    public void Back()
    {
        settingsPanel.SetActive(false);
        areYouSurePanel.SetActive(false);
    }
    public void Ok()
    {
        RankingUtils.ResetRanking();
        Back();
    }
}

