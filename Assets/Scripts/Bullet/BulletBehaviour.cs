using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe para controlar o comportamento da bala
/// </summary>
public class BulletBehaviour : MonoBehaviour
{
    //Referencia para o rigidbody da bala
    private Rigidbody2D Rb { get; set; }
    //Variavel de velocidade
    public int speed;
    //Direção da bala
    public Vector2 direction;
    // Use this for initialization
    void Start()
    {
        //Pega o component de ridgdbody
        Rb = GetComponent<Rigidbody2D>();
        //Move a bala para a direção atual
        Rb.AddForce(direction * speed * Time.deltaTime, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        //Verifica se pode destruir a bala
        CheckIfCanDestroy();

    }
    /// <summary>
    /// Metodo para destroir a bala
    /// </summary>
    /// <returns></returns>
    private bool CheckIfCanDestroy()
    {
        //Verifica se a bala está fora da tela
        if (!ScreenUtils.IsOnScreen(transform.position))
        {
            Destroy(this.gameObject);
            return true;
        }

        return false;
    }
}
