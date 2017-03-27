using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed;
    public bool dragging;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    //TODO: Implementar movimentação por touch TAMBÉM
    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        rb.MovePosition((Vector2)transform.position + (new Vector2(h, v) * Time.deltaTime * speed));
    }


}
