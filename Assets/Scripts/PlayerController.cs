using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float movementSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float climbSpeed;
    float defGravityScale;
    bool isAlive = true;
    [SerializeField]Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    Vector2 moveInput;
    Rigidbody2D rb2d;
    public SpriteRenderer spriteRenderer;
    Animator myAnim;
    CapsuleCollider2D playerCollider;
    BoxCollider2D playerFeetCollider;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myAnim = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
        defGravityScale = rb2d.gravityScale;
    }



    void Update()
    {
        if (isAlive)
        {
            Run();
            FlipSprite();
            ClimbLadder();
            Die();
        }
    }

    private void Die()
    {
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Enemy","Hazard")))
        {
            isAlive = false;
            rb2d.velocity = deathKick;
            myAnim.SetTrigger("Dying");
        }
    }

    public void OnMove(InputValue value)
    {
        if (isAlive)
        {
            moveInput = value.Get<Vector2>();
            Debug.Log("onmove " + moveInput);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * movementSpeed, rb2d.velocity.y);
        rb2d.velocity = playerVelocity;

        if (rb2d.velocity.x != 0)
        {
            myAnim.SetBool("isRunning", true);
        }
        else { myAnim.SetBool("isRunning", false); }
    }

    void FlipSprite()
    {
        if (rb2d.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (rb2d.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void OnJump(InputValue value)
    {
        if (isAlive)
        {
            if (playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Climbing")))
            {
                if (value.isPressed)
                {
                    Debug.Log("zýpla");
                    rb2d.velocity += new Vector2(0, jumpForce);
                }
            }
        }
    }

    void ClimbLadder()
    {
        if (playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            Vector2 climbVelocity = new Vector2(rb2d.velocity.x, moveInput.y * climbSpeed);
            rb2d.velocity = climbVelocity;
            rb2d.gravityScale = 0;

            if (rb2d.velocity.y != 0)
            {
                myAnim.SetBool("isClimbing",true);
            }
            else { myAnim.SetBool("isClimbing", false); }
        }
        else
        {
            rb2d.gravityScale = defGravityScale;
            return;
        }

    }

 /*   void OnFire(InputValue value)
    {
        if (isAlive)
        {
            if (value.isPressed)
            {
                Instantiate(bullet,gun.position, transform.rotation);
            }
        }
    }

    */



}
