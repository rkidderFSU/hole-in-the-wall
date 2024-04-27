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
            HandlePlayerMovement();
        }
    }

    private void Update()
    {
        if (cam.followPlayer && !m.gameEnded)
        {
            CheckMovementDirectionKeysPressed();
        }
        CheckMovementDirectionKeysReleased(); // Always checks for release of keys to prevent a bug with the sprite renderer
        animator.SetBool("Walking", rb.velocity.magnitude > 0.5f);
    }

    private void CheckMovementDirectionKeysPressed()
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
    }

    private void CheckMovementDirectionKeysReleased()
    {
        if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) && !movingRight)
        {
            movingLeft = false;
        }
        if ((Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) && !movingLeft)
        {
            movingRight = false;
        }
    }

    private void HandlePlayerMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0f);

        if (Mathf.Abs(rb.velocity.x) < maxSpeed)
        {
            rb.AddForce(movement * speed);
        }
    }
}
