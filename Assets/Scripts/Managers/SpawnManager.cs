using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : Singleton<SpawnManager>
{

    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    public float spawnTime;
    public int spawnLevel;
    public int reduceSpawntimeFactor;
    public float reduceSpawntimeAmount;

    private float startSpawnTime;
    private float timer;

    public Text spawnLevelText;

    private const string spawnLevelConstString = "Level: ";

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnRepeting());
        startSpawnTime = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanReduceSpawnRatio())
        {
            Debug.Log("Mudou");
        }
    }

    public void SpawnEnemy()
    {

        int rand = Random.Range(0, enemiesToSpawn.Count);
        Vector2 worldBoundPosX = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.width));
        Vector2 worldBoundPosY = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
        Vector2 spawnPos = new Vector2(worldBoundPosX.y,
                                      Random.Range(worldBoundPosY.x + 2, worldBoundPosY.y - 2));



        Instantiate(enemiesToSpawn[rand], spawnPos, enemiesToSpawn[rand].transform.rotation);

    }

    public IEnumerator SpawnRepeting()
    {
        yield return new WaitForSeconds(spawnTime);

        if (enemiesToSpawn.Count > 0)
            SpawnEnemy();
        else Debug.LogWarning("enemiesToSpawn is empty");

        StartCoroutine(SpawnRepeting());
    }

    public bool CanReduceSpawnRatio()
    {
        int score = GameManager.Instance.score;
        int newSpawnLevel = score / reduceSpawntimeFactor;

        bool can = newSpawnLevel > spawnLevel ? true : false;
        if (can)
        {

            spawnLevel = newSpawnLevel > spawnLevel ? newSpawnLevel : spawnLevel;
            spawnTime = startSpawnTime - (reduceSpawntimeAmount * spawnLevel);
            spawnTime = Mathf.Clamp(spawnTime, 0.5f, Mathf.Infinity);

            spawnLevelText.text = spawnLevelConstString + spawnLevel;
            return true;
        }

        return false;




    }


}
