using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        while (!async.isDone)
        {
            yield return null;
        }
    }
}
