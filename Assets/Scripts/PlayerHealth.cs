using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public int maxHealth = 4;
    public int currentHealth; // Initialize this in the Inspector or Start method
    public Sprite[] wear; // Array of sprites for different player states
    public float invincibilityDuration = 0.5f; // Duration of invincibility in seconds
    public int maxInvincibilityFrames = 2; // Number of invincibility frames

    private bool isInvincible = false;
    private int invincibilityFrameCount = 0;
    private float invincibilityTimer = 0f;
    private Collider2D lastCollisionEnemy; // Store the last enemy that collided with the player
    private float delayBetweenDamage = 1.0f; // Adjust this as needed
    private float timeSinceLastCollision = 0f;

    public bool playerIsAlive = true;

    public GameManager game;

    public GameObject healthPickup;

    public void SetPlayerIsAlive(bool isAlive)
    {
        playerIsAlive = isAlive;
    }

    public bool GetPlayerIsAlive()
    {
        return playerIsAlive;
    }

    void ChangeSprite(int health)
    {
        if (health >= 0 && health < wear.Length)
        {
            spriteRenderer.sprite = wear[health];
        }
    }

    void Start()
    {   
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        currentHealth = maxHealth; // Initialize health
        ChangeSprite(currentHealth); // Set the initial sprite based on player's health

        FindObjectOfType<AudioManager>().Play("Music");
    }

    void Update()
    {
        if (isInvincible)
        {
            invincibilityTimer += Time.deltaTime;
            if (invincibilityTimer >= invincibilityDuration)
            {
                isInvincible = false;
                invincibilityFrameCount++;
                invincibilityTimer = 0f;
                spriteRenderer.color = Color.white; // Restore full opacity
            }
        }

        if (timeSinceLastCollision < delayBetweenDamage)
        {
            timeSinceLastCollision += Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {   

        if (collision.gameObject.CompareTag("Health") && currentHealth < maxHealth)
        {   
            Destroy(collision.gameObject);
            currentHealth++;
            currentHealth = Mathf.Clamp(currentHealth, 0, wear.Length - 1);
            ChangeSprite(currentHealth); 
            FindObjectOfType<AudioManager>().Play("Powerup");
        }

        if (isInvincible)
            return; // Don't take damage while invincible

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (lastCollisionEnemy == null || lastCollisionEnemy.gameObject != collision.gameObject || timeSinceLastCollision >= delayBetweenDamage)
            {
                lastCollisionEnemy = collision.collider;
                currentHealth--;
                currentHealth = Mathf.Clamp(currentHealth, 0, wear.Length - 1);
                ChangeSprite(currentHealth);

                if (playerIsAlive == true)
                {
                    FindObjectOfType<AudioManager>().Play("PlayerDamage");
                }

                if (currentHealth == 0 && playerIsAlive == true) // Game over condition
                {
                    FindObjectOfType<AudioManager>().Play("GameOver");
                    game.GameOver();
                    playerIsAlive = false;
                }
                else
                {
                    isInvincible = true;
                    invincibilityTimer = 0f;
                    spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f); // Set player to half opacity
                    StartCoroutine(FlashPlayer());
                }
                timeSinceLastCollision = 0f;
            }
        } else if (IsTouchingEnemy(lastCollisionEnemy) && !isInvincible && playerIsAlive == true)
        {
            currentHealth--;
            currentHealth = Mathf.Clamp(currentHealth, 0, wear.Length - 1);
            ChangeSprite(currentHealth);
            FindObjectOfType<AudioManager>().Play("PlayerDamage");
            if (currentHealth == 0 && playerIsAlive == true) // Game over condition
                {
                    FindObjectOfType<AudioManager>().Play("GameOver");
                    game.GameOver();
                    playerIsAlive = false;
                }
                else
                {
                    isInvincible = true;
                    invincibilityTimer = 0f;
                    spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f); // Set player to half opacity
                    StartCoroutine(FlashPlayer());
                }
                timeSinceLastCollision = 0f;
        }
    }

    IEnumerator FlashPlayer()
    {
        for (int i = 0; i < maxInvincibilityFrames * 2; i++)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // Toggle sprite visibility
            yield return new WaitForSeconds(invincibilityDuration / 2);
        }

        spriteRenderer.enabled = true; // Ensure the sprite is visible
    }

    // Check if the player is still touching a specific enemy
    bool IsTouchingEnemy(Collider2D enemyCollider)
    {
        return gameObject.GetComponent<Collider2D>().IsTouching(enemyCollider);
    }
}