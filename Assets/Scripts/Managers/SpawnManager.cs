using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Classe que controla o spawn dos inimigos
/// </summary>
public class SpawnManager : Singleton<SpawnManager>
{
    //Lista com prefabs dos inimigos para spawnar
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    public GameObject boss;
    //Variaveis para controlar tempo de spawn
    public float spawnTime;
    public int reduceSpawntimeFactor;
    public float reduceSpawntimeAmount;
    private float startSpawnTime;
    private float timer;
    //Variaveel para controlar o level de spawn
    public int spawnLevel;

    //Referencia do texto de level na cena
    public Text spawnLevelText;
    //Referencia para o background na cena
    public GameObject background;

    private const string spawnLevelConstString = "Level: ";
    private Bounds backgroundBounds;

    public bool canSpawn;

    // Use this for initialization
    void Start()
    {
        //Executa a corotina para spawnar os inimigos
        StartCoroutine(SpawnRepeting());
        startSpawnTime = spawnTime;
        //pega as bounds do bakcground
        backgroundBounds = background.GetComponent<SpriteRenderer>().bounds;
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Verifica se o estado atual é GAME
        if (GameManager.Instance.IsCurrentGameState(GAMESTATES.GAME))
            ReduceSpawnRatio();

    }

    /// <summary>
    /// Metodo para spawnar inimigo
    /// </summary>
    public void SpawnEnemy()
    {
        //Pega um valor aleatorio dentr 0 e o numero de elementos da lista de inimigos
        int rand = Random.Range(0, enemiesToSpawn.Count);
        //Calcula o tamanho do inimigo e bounds da tela
        Bounds enemyBounds = enemiesToSpawn[rand].GetComponent<SpriteRenderer>().bounds;
        Vector2 enemySize = new Vector2(enemyBounds.max.x - enemyBounds.min.x, enemyBounds.max.y - enemyBounds.min.y);
        Vector2 screenBoundsX = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.width));

        //Calcula a posição que o inimigo vai nascer
        Vector2 spawnPos = new Vector2(screenBoundsX.y - enemySize.x,
                                      Random.Range(backgroundBounds.min.y + enemySize.y, backgroundBounds.max.y - enemySize.y));


        //Instancia o inimigo na cena
        Instantiate(enemiesToSpawn[rand], spawnPos, enemiesToSpawn[rand].transform.rotation);

    }

    public void SpawnBoss()
    {
        Bounds enemyBounds = boss.GetComponent<SpriteRenderer>().bounds;
        Vector2 enemySize = new Vector2(enemyBounds.max.x - enemyBounds.min.x, enemyBounds.max.y - enemyBounds.min.y);
        Vector2 screenBoundsX = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.width));
        Vector2 spawnPos = new Vector2(screenBoundsX.y - enemySize.x,
                                      Random.Range(backgroundBounds.min.y + enemySize.y, backgroundBounds.max.y - enemySize.y));

        Instantiate(boss, spawnPos, boss.transform.rotation);

    }

    /// <summary>
    /// Corotina para spawnar inimgios
    /// </summary>
    /// <returns></returns>
    public IEnumerator SpawnRepeting()
    {
        //Espera o tempo de spawn
        yield return new WaitForSeconds(spawnTime);

        //Verifica se a quantidade de inimigos na lista é maior que 0
        //e o estado atual de jogo é GAME
        if (enemiesToSpawn.Count > 0 && GameManager.Instance.IsCurrentGameState(GAMESTATES.GAME) && canSpawn)
            SpawnEnemy();

        //Executa a corotina novamente
        StartCoroutine(SpawnRepeting());
    }

    /// <summary>
    /// Metodo para calcular a redução de tempo caso o player mude de level
    /// </summary>
    public void ReduceSpawnRatio()
    {
        //Pega o score atual
        int score = GameManager.Instance.score;
        //Calcula o novo level
        int newSpawnLevel = score / reduceSpawntimeFactor;
        //Verifica se o novo level é maior que o level
        bool can = newSpawnLevel > spawnLevel ? true : false;
        if (can)
        {
            SpawnBoss();
            //Coloca o novo level na variavel de de level atual
            spawnLevel = newSpawnLevel > spawnLevel ? newSpawnLevel : spawnLevel;
            //Reduz o tempo de spawn
            spawnTime = startSpawnTime - (reduceSpawntimeAmount * spawnLevel);
            //Não deixa o tempo de spawn ser menor que 0.5 segundos
            spawnTime = Mathf.Clamp(spawnTime, 0.5f, Mathf.Infinity);
            //Muda o texto de level
            spawnLevelText.text = spawnLevelConstString + spawnLevel;
            //Executa o som de passar de level
            SoundManager.Instance.PlaySfx("Dif_Up");
            if (spawnLevel % 2 == 0)
            {
                GameManager.instance.player.hp++;
            }

        }

    }


}
