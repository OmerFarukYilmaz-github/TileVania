using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Rigidbody2D rb2D;
    SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2D.velocity = new Vector2(moveSpeed, 0);
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipEnemySprite();
    }

    private void FlipEnemySprite()
    {
        if (rb2D.velocity.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (rb2D.velocity.x < 0)
        {
            spriteRenderer.flipX = false;
        }

    }
}
