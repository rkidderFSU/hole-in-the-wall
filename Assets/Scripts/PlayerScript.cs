using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float acceleration = 20f;
    public float maxSpeed = 10f;
    private bool movingLeft;
    private bool movingRight;
    private SpriteRenderer sr;
    private Animator animator;
    private CameraController cam;
    private GameManager m;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
        m = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }


    void FixedUpdate()
    {
        if (cam.followPlayer && !m.gameEnded)
        {
            // Handle movement
            float moveHorizontal = Input.GetAxis("Horizontal");
            Vector2 movement = new Vector2(moveHorizontal, 0f);

            if (Mathf.Abs(rb.velocity.x) < maxSpeed)
            {
                rb.AddForce(movement * acceleration);
            }
        }
    }

    private void Update()
    {
        if (cam.followPlayer && !m.gameEnded)
        {
            CheckMovementDirection();
        }
        animator.SetBool("Walking", rb.velocity.magnitude > 0.5f);
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
