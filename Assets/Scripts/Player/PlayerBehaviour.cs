using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Classe para controlar o comportamento do player, por toque ou controle
/// </summary>
public class PlayerBehaviour : MonoBehaviour
{
    //Variaveis para controlar a invunerabilidade
    private int invTimesMax, invTimes;

    //Referencia para o rigidbody do player
    private Rigidbody2D rb;
    //Referencia para onde o tiro do player vai sair
    private Transform Gun { get; set; }
    //Bounds para delimitar onde o player não pode passar
    private Bounds backgroundBounds, playerBounds;
    //Variavel para guardar o tamanho do player em coordenadas de mundo
    private Vector2 playerSize;
    //Variaveis para velocidade, tempo para cada tiro do player sair e tempo de invunerabilidade
    public float speed, fireRatio, invulnerabilityTime;
    //Variaveis para verificar se o jogador está com o dedo carregando o player,
    //para ver se o player pode atirar e se ele está invuneravel
    public bool dragging, canShoot, invulnerability;
    //Variaveis para guardar o hp maximo e atual do player
    public int maxHp, hp;
    //Variaveis parar guardar a prefab de bala e referencia para o background na cena
    public GameObject bullet, background;
    //Referencia para o texto de hp na cena
    public Text hpText;


    // Use this for initialization
    void Start()
    {

        Gun = transform.FindChild("Gun");
        rb = GetComponent<Rigidbody2D>();
        canShoot = true;
        hp = maxHp;
        invTimesMax = 3;
        invTimes = 0;
        invulnerability = false;

        //Pega as bounds do background, player e o calcula o tamanho do player
        backgroundBounds = background.GetComponent<SpriteRenderer>().bounds;
        playerBounds = GetComponent<SpriteRenderer>().bounds;
        playerSize = new Vector2(playerBounds.max.x - playerBounds.min.x, playerBounds.max.y - playerBounds.min.y);

    }

    private void Update()
    {
        //Verifica se o estado atual do jogo é GAME
        if (GameManager.Instance.IsCurrentGameState(GAMESTATES.GAME))
        {
            //Verifica se o player está morto
            if (IsDead())
            {
                //Seta o estado de jogo para mostrar o rank
                GameManager.Instance.SetStateToRank();
                //Toca um SFX de explosão do player
                SoundManager.Instance.PlaySfx("player_Explosion");
            }
            //Limita onde o player pode ir
            LimitPlayerOnBackground();
            //Faz o player atirar
            Shoot();
            //Atribui o texto de UI de hp para o hp atual
            hpText.text = "x " + hp;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Verifica se o estado atual do jogo é GAME
        if (GameManager.Instance.IsCurrentGameState(GAMESTATES.GAME))
        {
            //Movimenta o jogador por controle
            Move();
            //Movimenta o jogador pelo touch
            TouchMove();
        }

    }

    /// <summary>
    /// Metodo para subtrair a vida do player
    /// </summary>
    /// <param name="amount"></param>
    public void SubHp(int amount)
    {
        hp -= amount;
    }

    /// <summary>
    /// Metodo para movimentar o player por joystick ou teclado
    /// </summary>
    private void Move()
    {
        //Pega os inputs do eixo horizontal e vertical
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //Move o player por rigdibody
        rb.MovePosition((Vector2)transform.position + (new Vector2(h, v) * Time.deltaTime * speed));
    }
    /// <summary>
    /// Metodo para movimentar o player por toque
    /// </summary>
    private void TouchMove()
    {
        //Verifica se tem algum toque na tela
        if (Input.touchCount > 0)
        {
            //Pega o primeiro toque
            Touch touch = Input.GetTouch(0);
            //Guarda a posição do toque em coordenada de mundo
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            //Verifica se a fase do toque é logo quando toca na tela
            if (touch.phase == TouchPhase.Began)
            {
                //Verifica se o toque foi em cima da nave
                if (Physics2D.Raycast(touchPos, touchPos, 0.01f, 1 << LayerMask.NameToLayer("Player")))
                {
                    dragging = true;
                }
            }
            //Verifica se o jogador tirou o dedo da tela
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                dragging = false;
            }
            //Verifica se o jogador esta movendo o dedo pela tela e se o dragging é true
            if (Input.GetTouch(0).phase == TouchPhase.Moved && dragging)
            {
                //Seta a posição do jogador para a posição do dedo na tela
                transform.position = touchPos;
            }


        }
    }
    /// <summary>
    /// Metodo para o jogador atirar, tanto no joystick quanto no toque
    /// </summary>
    private void Shoot()
    {
        //Verifica se o input de atirar foi ativado e o jogador pode atirar
        if (Input.GetAxis("Fire1") != 0 && canShoot)
        {
            //Instancia a bala no ponto onde a arma está colocada
            GameObject aux = Instantiate(bullet, Gun.position, bullet.transform.rotation);
            //Seta a direção da bala
            aux.GetComponent<BulletBehaviour>().direction = Vector2.right;
            //Executa a corotina para deixar o tiro em cooldown
            StartCoroutine(FireRatioCooldown());
            //Executa o som de tiro
            SoundManager.Instance.PlaySfx("tiro2");
        }
    }
    /// <summary>
    /// Corotina para deixar o tiro em cooldown
    /// </summary>
    /// <returns></returns>
    private IEnumerator FireRatioCooldown()
    {
        canShoot = false;
        //Espera o tempo sem segundos conrrespondente a variavel fireRatio
        yield return new WaitForSeconds(fireRatio);

        canShoot = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Verifica se tocou em um inimigo ou bala de inimigo se ele n estiver invuneravel e o estado de jogo atual for GAME
        if (collision.tag == "Enemy" || collision.tag == "EnemyBullet" && !invulnerability && GameManager.Instance.IsCurrentGameState(GAMESTATES.GAME))
        {
            //Tira um de hp do player
            SubHp(1);
            //Toca o som de tomar tiro se o jogador n estiver morto
            if (!IsDead()) SoundManager.Instance.PlaySfx("player_Hit");
            //Deixa o jogador invuneravel
            invulnerability = true;
            //Executa a corotina para o jogador ficar piscando
            StartCoroutine(InvulnerabilityCoroutine());

        }
    }

    /// <summary>
    /// Corotina para fazer o jogador ficar piscando
    /// </summary>
    /// <returns></returns>
    private IEnumerator InvulnerabilityCoroutine()
    {
        //Referencia para o SpriteRenderer do player
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        //Guarda a cor antiga e a nova
        Color oldColor = Color.white;
        Color newColor = oldColor;

        //Verifica se a quantidade de vezes que o jogador ficou invisivel 
        //é menor que o total de vezes que ele pode
        if (invTimes <= invTimesMax)
        {
            //Seta o alpha da nova cor para 0;
            newColor.a = 0;
            //Seta a cor do renderer para a nova cor
            renderer.color = newColor;

            //Espera o tempo de invunerabilidade pela metade
            yield return new WaitForSeconds(invulnerabilityTime / 2);
            //Muda a cor do player para a cor antiga
            renderer.color = oldColor;
            //Espera o tempo de invunerabilidade pela metade
            yield return new WaitForSeconds(invulnerabilityTime / 2);
            //Aumenta a quantidade de vezes que ele ficou invisivel
            invTimes++;
            //Repete a corotina
            StartCoroutine(InvulnerabilityCoroutine());

        }
        else
        {
            //Certifica que a cor do player é a antiga
            renderer.color = oldColor;
            //Muda a quantidade de vezes que ele ficou invisivel para 0
            invTimes = 0;
            //Seta a invunerabilidade para falso
            invulnerability = false;
        }

    }
    /// <summary>
    /// Verifica se o jogador está morto
    /// </summary>
    /// <returns></returns>
    public bool IsDead()
    {
        return hp <= 0;
    }
    /// <summary>
    /// Limita o jogador dentro do background
    /// </summary>
    public void LimitPlayerOnBackground()
    {

        Vector2 playerPos = transform.position;

        playerPos.x = Mathf.Clamp(playerPos.x, backgroundBounds.min.x + playerSize.x / 2, backgroundBounds.max.x - playerSize.x / 2);
        playerPos.y = Mathf.Clamp(playerPos.y, backgroundBounds.min.y + playerSize.y / 2, backgroundBounds.max.y - playerSize.y / 2);

        transform.position = playerPos;

    }
}
