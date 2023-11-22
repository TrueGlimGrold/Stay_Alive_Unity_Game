using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public GameObject activePlayer;

    public bool playerIsAlive;
    private float moveSpeed = 5f;

    private int speedLevel; 

    public Rigidbody2D rb;

    public Camera cam;

    Vector2 movement; 
    Vector2 mousePos;

    string currentSceneName;

    void Start()
    {
        speedLevel = PlayerPrefs.GetInt("speed");

        currentSceneName = SceneManager.GetActiveScene().name;

        // Only allow movement along the non-zero axis
        if (currentSceneName == "MainMenu")
        {
            moveSpeed = 0;
        } 
        else {
            moveSpeed = 5f + (speedLevel * 0.4f); 
        }
    }

    // Update is called once per frame
    void Update()
    {   
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {   
        PlayerHealth playerHealth = activePlayer.GetComponent<PlayerHealth>();
        playerIsAlive = playerHealth.GetPlayerIsAlive();
        if (playerIsAlive)
        {   
            // Use Physics2D.MovePosition to move the player and handle collisions
            Vector2 newPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);

            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }
}
