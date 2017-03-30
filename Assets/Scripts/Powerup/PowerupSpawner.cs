using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : Singleton<PowerupSpawner>
{

    public List<GameObject> powerupsList = new List<GameObject>();


    public void SpawnPowerup(Vector2 position, int chance)
    {
        //Certificar que a chance está entre 0 e 100%
        chance = Mathf.Clamp(chance, 0, 100);

        int randChance = Random.Range(0, 100);

        if (randChance < chance)
        {
            int randPowerup = Random.Range(0, powerupsList.Count);
            Instantiate(powerupsList[randPowerup], position, powerupsList[randPowerup].transform.rotation);
        }

    }
}
