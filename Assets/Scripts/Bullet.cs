using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameManager game;
    
    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an object tagged as "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // Destroy the enemy
            Destroy(gameObject);
            game.addScore(300); 
            FindObjectOfType<AudioManager>().Play("RobotDeath");
        }

        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {   
            Destroy(gameObject);
        }
    }

}
