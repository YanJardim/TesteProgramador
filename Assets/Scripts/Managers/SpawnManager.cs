using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public int spawnTime;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    public int SpawnFactor { get; set; }

    private float timer;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnRepeting());
    }

    // Update is called once per frame
    void Update()
    {
        if (CanReduceSpawnRatio())
        {

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

        SpawnEnemy();

        StartCoroutine(SpawnRepeting());
    }

    public bool CanReduceSpawnRatio()
    {
        int score = GameManager.Instance.score;
        score /= 500;
        bool result = score % 1 == 0;
        if (result)
        {
            return true;
        }

        return false;
    }


}
