using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 10f;
    public float maxSpeed = 10f;
    private bool movingLeft;
    private bool movingRight;
    private SpriteRenderer sr;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
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

    private void Update()
    {
        CheckMovementDirection();
    }

    private void CheckMovementDirection()
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !movingRight)
        {
            movingLeft = true;
            sr.flipX = true;
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !movingLeft)
        {
            movingRight = true;
            sr.flipX = false;
        }
        if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) && !movingRight)
        {
            movingLeft = false;
        }
        if ((Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) && !movingLeft)
        {
            movingRight = false;
        } 
    }
}
