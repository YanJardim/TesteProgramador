using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public int spawnTime;
    public List<GameObject> enemysToSpawn = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnRepeting());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnEnemy()
    {
        int rand = Random.Range(0, enemysToSpawn.Count);
        Vector2 worldBoundPosX = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.width));
        Vector2 worldBoundPosY = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
        Vector2 spawnPos = new Vector2(worldBoundPosX.y,
                                      Random.Range(worldBoundPosY.x + 2, worldBoundPosY.y - 2));

        GameObject newEnemy = Instantiate(enemysToSpawn[rand], spawnPos, enemysToSpawn[rand].transform.rotation);

    }

    public IEnumerator SpawnRepeting()
    {
        yield return new WaitForSeconds(spawnTime);

        SpawnEnemy();

        StartCoroutine(SpawnRepeting());
    }


}
