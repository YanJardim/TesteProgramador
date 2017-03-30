using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEffect : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (!GameManager.Instance.player.powerupShield)
        {
            GameManager.Instance.player.ChangePowerup();
            GameManager.Instance.player.powerupShield = true;
        }
        else GameManager.Instance.player.powerupTimerMax += 10;

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.player.powerupShield)
        {
            GameManager.Instance.player.powerupUi.SetActive(false);
            StartCoroutine(StopInvulnerability());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "EnemyBullet")
        {

            if (collision.tag == "EnemyBullet")
            {
                Destroy(collision.gameObject);
            }
            GameManager.Instance.player.powerupUi.SetActive(false);
            StartCoroutine(StopInvulnerability());
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public IEnumerator StopInvulnerability()
    {

        yield return new WaitForSeconds(1);

        GameManager.Instance.player.powerupShield = false;
        Destroy(this.gameObject);
    }

}


