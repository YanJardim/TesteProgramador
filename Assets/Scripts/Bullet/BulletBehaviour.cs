using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    public bool direction;
    private Rigidbody2D Rb { get; set; }
    public int speed;
    // Use this for initialization
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Rb.AddForce(Vector2.right * (direction ? -speed : speed) * Time.deltaTime, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfCanDestroy();
    }

    private bool CheckIfCanDestroy()
    {

        if (!ScreenUtils.CheckIfIsOnScreen(transform.position))
        {
            Destroy(this.gameObject);
            return true;
        }

        return false;
    }
}
