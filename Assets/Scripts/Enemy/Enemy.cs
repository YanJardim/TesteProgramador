using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Estados para a maquina de estados
/// </summary>
public enum STATE { ATTACK, IDLE, TRIGGER };

/// <summary>
/// Classe abstrata para criar varios tipos de inimigos
/// </summary>
public abstract class Enemy : MonoBehaviour
{
    //Estado atual
    protected STATE CurrentState { get; set; }
    //RigidBody para usar as funções de fisica
    protected Rigidbody2D Rb { get; set; }
    //Referencia para o player 
    protected GameObject Player { get; set; }

    //HP do inimigo
    public int hp;

    //Pontos que o inimigo vai dar ao jogador quando morrer
    public int points;

    //Chance do inimigo ir para o estado de Attack
    [Range(0, 100)]
    public int attackChance, spawnPowerupChance;

    //Velocidade do inimigo
    public float speed;


    // Use this for initialization
    protected void Start()
    {
        //Mudando o estado atual para Idle
        CurrentState = STATE.IDLE;
        //Pegando a referencia do rigidbody
        Rb = GetComponent<Rigidbody2D>();
        //Pegando a referencia do player
        Player = GameObject.FindGameObjectWithTag("Player");
        //Inicializando a corotina
        StartCoroutine(TriggerAttack());
        //Operação ternaria para ver se o hp é menor que 0, caso for muda para 1
        hp = hp <= 0 ? 1 : hp;

    }
    /// <summary>
    /// Metodo abstrato para o estado de Attack (quando o inimigo entra em modo de ataque)
    /// </summary>
    public abstract void OnAttack();
    /// <summary>
    /// Metodo abstrato para o estado de Idle (quando o inimigo nasce)
    /// </summary>
    public abstract void OnIdle();
    /// <summary>
    /// Metodo abstrato para o estado de Trigger (quando o inimigo é antigido)
    /// </summary>
    public abstract void OnTrigger();


    /// <summary>
    /// Metodo para mudar o estado atual do inimigo
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(STATE newState)
    {
        CurrentState = newState;
    }

    /// <summary>
    /// Metodo Para executar a maquina de estados
    /// </summary>
    protected void ExecuteFSM()
    {
        if (GameManager.Instance.IsCurrentGameState(GAMESTATES.GAME))
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Caso a bala do jogador atinga o inimigo
        if (collision.tag == "PlayerBullet")
        {




            //Destroi a bala
            Destroy(collision.gameObject);

            //Muda o estado do inimigo para Trigger
            ChangeState(STATE.TRIGGER);

            //Tira vida do inimigo e caso a vida for 0 destroi ele
            SubLife();
        }
    }

    /// <summary>
    /// Corotina para ativar o estado de Trigger
    /// </summary>
    /// <returns></returns>
    protected IEnumerator TriggerAttack()
    {
        //Espera 0.5 segundos
        yield return new WaitForSeconds(0.5f);

        //Verifica se o estado atual é Idle ou a distancia entre ele e o player é menor que 2
        if (CurrentState == STATE.IDLE && GetDistanceToPlayer() > 2)
        {
            //Cria um numero randomico entre 1 e 100
            int rand = Random.Range(0, 100);


            //Verifica se o numero randomico é menor que a chance de mudar de estado
            if (rand <= attackChance)
            {
                //Muda de estado para Attack
                ChangeState(STATE.ATTACK);
            }
            else
            {
                //Caso o numero randomico seja maior a corotina é chamada novamente
                StartCoroutine(TriggerAttack());
            }
        }
    }

    /// <summary>
    /// Metodo para retornar a distancia entre esse inimigo e o player
    /// </summary>
    /// <returns></returns>
    public float GetDistanceToPlayer()
    {
        return Vector2.Distance(transform.position, Player.transform.position);
    }

    /// <summary>
    /// Metodo para verificar se o inimigo está do lado esquerdo da camera
    /// </summary>
    /// <returns></returns>
    public bool DestroyEnemy()
    {

        //Verifica se o inimigo está do lado esquerdo da camera (fora da tela)
        if (ScreenUtils.IsOnLeftOfTheCamera(transform.position))
        {
            //Destroi o inimigo e retorna true
            Destroy(this.gameObject);
            return true;
        }


        return false;
    }

    /// <summary>
    /// Metodo para subtrair vida do inimigo, caso seja menor que 0 ele é destroido 
    /// </summary>
    public void SubLife()
    {
        hp--;
        if (hp <= 0)
        {
            PowerupSpawner.Instance.SpawnPowerup(transform.position, spawnPowerupChance);

            //Adiciona pontos na variavel Score do GameManager
            SoundManager.Instance.PlaySfx("explosion1");
            GameManager.Instance.AddScore(points);
            Destroy(this.gameObject);
        }
    }

}
