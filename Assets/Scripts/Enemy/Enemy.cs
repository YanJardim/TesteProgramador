using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum STATE { ATTACK, IDLE, TRIGGER };


public abstract class Enemy : MonoBehaviour
{

    protected STATE CurrentState { get; set; }
    protected Rigidbody2D Rb { get; set; }
    protected GameObject Player { get; set; }


    public int Hp { get; set; }

    [Range(0, 100)]
    public int attackChance;

    public float speed;

    // Use this for initialization
    protected void Start()
    {
        CurrentState = STATE.IDLE;
        Rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(TriggerAttack());
        Hp = 0;
    }



    public abstract void OnAttack();
    public abstract void OnIdle();
    public abstract void OnTrigger();

    public void ChangeState(STATE newState)
    {
        CurrentState = newState;
    }

    protected void ExecuteFSM()
    {
        switch (CurrentState)
        {
            case STATE.ATTACK:
                OnAttack();
                break;
            case STATE.IDLE:

                OnIdle();

                break;
            case STATE.TRIGGER:
                OnTrigger();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            ChangeState(STATE.TRIGGER);
        }
    }

    protected IEnumerator TriggerAttack()
    {
        Debug.Log("Na corrotina");
        yield return new WaitForSeconds(0.5f);

        Debug.Log("Distance: " + GetDistanceToPlayer());
        if (CurrentState == STATE.IDLE && GetDistanceToPlayer() > 2)
        {
            int rand = Random.Range(0, 100);
            Debug.Log("Rand: " + rand);
            if (rand <= attackChance)
            {
                Debug.Log("Troca de estado para Attack   " + rand);
                ChangeState(STATE.ATTACK);
            }
            else
            {
                StartCoroutine(TriggerAttack());
            }
        }
    }

    public float GetDistanceToPlayer()
    {
        return Vector2.Distance(transform.position, Player.transform.position);
    }

    public bool DestroyEnemy()
    {
        if (transform.position.x < -8)
        {
            Destroy(this.gameObject);
            return true;
        }


        return false;
    }

}
