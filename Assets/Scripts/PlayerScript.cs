using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 10f;
    public float maxSpeed = 10f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");


        Vector2 movement = new Vector2(moveHorizontal, 0f);


        if (Mathf.Abs(rb.velocity.x) < maxSpeed)
        {
            rb.AddForce(movement * speed);
        }

    }
}
