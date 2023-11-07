using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{   
    public bool playerIsAlive;
    public GameObject activePlayer;
    public Transform firepoint;
    
    public GameObject bulletPreFab;

    public float bulletForce = 20f;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
            if (playerIsAlive)
            {
                FindObjectOfType<AudioManager>().Play("Shoot");
            }
        }
    }

    void Shoot()
    {   
        PlayerHealth playerHealth = activePlayer.GetComponent<PlayerHealth>();
        playerIsAlive = playerHealth.GetPlayerIsAlive();
        if (playerIsAlive)
        {
            // Calculate the direction from the firepoint to the mouse position
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2)firepoint.position).normalized;

            // Calculate the rotation based on the direction
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion bulletRotation = Quaternion.AngleAxis(angle, Vector3.forward);

            GameObject bullet = Instantiate(bulletPreFab, firepoint.position, bulletRotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);
        }
    }
}
