using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb2D;
    [SerializeField] float bulletSpeed;
    PlayerController playerController;
    
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.spriteRenderer.flipX==false)
        rb2D.velocity = new Vector2(bulletSpeed ,0f);
        else
        rb2D.velocity = new Vector2(-bulletSpeed, 0f);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("d‹ﬁMANA CARPTI");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

     public void OnCollisionEnter2D(Collision2D other)
      {
        if(other.gameObject.tag=="Ground")
          Destroy(gameObject);
      }
   
}
