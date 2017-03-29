using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    private Rigidbody2D Rb { get; set; }
    public int speed;

    public Vector2 direction;
    // Use this for initialization
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Rb.AddForce(direction * speed * Time.deltaTime, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfCanDestroy();

    }

    private bool CheckIfCanDestroy()
    {

        if (!ScreenUtils.IsOnScreen(transform.position))
        {
            Destroy(this.gameObject);
            return true;
        }

        return false;
    }
}
