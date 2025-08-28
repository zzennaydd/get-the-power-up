using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    float y;
    float x;
    public float speed = 10;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(x * speed, rb.velocity.y);
        rb.velocity = new Vector2(rb.velocity.x, y * speed);
    }
}
