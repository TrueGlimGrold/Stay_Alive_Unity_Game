using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    private Transform target;
    private Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed = 5f;
    public bool isGhost;
    // Start is called before the first frame update
    public float maxHealth = 1f;
    private float currentHealth = 1f;

    public GameManager game;

    public Animator animator;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

        // Find the GameManager to access the active player reference
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            target = gameManager.activePlayer.transform;
        }

        if (isGhost)
        {
            // If the enemy is a ghost, set its collision layers to ignore "enemy" and "wall" layers.
            gameObject.layer = LayerMask.NameToLayer("Ghost"); // Create a new layer called "Ghost".
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ghost"), LayerMask.NameToLayer("Enemies"));
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ghost"), LayerMask.NameToLayer("Walls"));
        }

        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;

        animator.SetFloat("CurrentHealth", currentHealth);
    }

    private void FixedUpdate() {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction){
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {   
        // Check if the collision is with an object tagged as "Enemy"
        if (collision.gameObject.CompareTag("Bullet") && currentHealth <= 1f)
        {   
            // collision.gameObject.SetActive(false);
            Destroy(collision.gameObject); 
            Destroy(gameObject);
            game.addScore(300); 
            FindObjectOfType<AudioManager>().Play("RobotDeath");
        } else if (collision.gameObject.CompareTag("Bullet") && currentHealth > 1f)
        {
            currentHealth -= 1;
            FindObjectOfType<AudioManager>().Play("GiantHit");
        }
    }
}
