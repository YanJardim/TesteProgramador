using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    //Referencia para o player


    // Use this for initialization
    public void Start()
    {
        //Pega o script de comportar do player

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Effect();
            Destroy(this.gameObject);
        }
    }

    //Metodo para executar o efeito do powerup
    public abstract void Effect();
    /*public abstract void Draw();
    public abstract void Disable();*/
}
