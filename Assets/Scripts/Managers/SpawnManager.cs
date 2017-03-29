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
    public GameObject background;

    private const string spawnLevelConstString = "Level: ";
    private Bounds backgroundBounds;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnRepeting());
        startSpawnTime = spawnTime;
        backgroundBounds = background.GetComponent<SpriteRenderer>().bounds;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsCurrentGameState(GAMESTATES.GAME))
            ReduceSpawnRatio();

    }

    public void SpawnEnemy()
    {
        //TODO: passar parte disso pro enemy
        int rand = Random.Range(0, enemiesToSpawn.Count);
        Bounds enemyBounds = enemiesToSpawn[rand].GetComponent<SpriteRenderer>().bounds;
        Vector2 enemySize = new Vector2(enemyBounds.max.x - enemyBounds.min.x, enemyBounds.max.y - enemyBounds.min.y);
        Vector2 screenBoundsX = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.width));


        Vector2 spawnPos = new Vector2(screenBoundsX.y,
                                      Random.Range(backgroundBounds.min.y + enemySize.y, backgroundBounds.max.y - enemySize.y));



        Instantiate(enemiesToSpawn[rand], spawnPos, enemiesToSpawn[rand].transform.rotation);

    }

    public IEnumerator SpawnRepeting()
    {
        yield return new WaitForSeconds(spawnTime);

        if (enemiesToSpawn.Count > 0 && GameManager.Instance.IsCurrentGameState(GAMESTATES.GAME))
            SpawnEnemy();

        StartCoroutine(SpawnRepeting());
    }

    public void ReduceSpawnRatio()
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

        }

    }


}
