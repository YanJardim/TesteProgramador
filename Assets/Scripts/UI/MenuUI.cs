using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI
{
    /// <summary>
    /// Metodo para carregar a cena Game
    /// </summary>
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
}
