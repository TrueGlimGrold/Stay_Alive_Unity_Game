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

    private float firerate;
    float nextfire; 

    private float normalFireRate;
    private float starPowerupFireRate;

    private int fireRateLevel;

    void Start()
    {
        fireRateLevel = PlayerPrefs.GetInt("fireRate");
        normalFireRate = 0.35f - (fireRateLevel * 0.01875f); 
        starPowerupFireRate = normalFireRate / 100;
        firerate = normalFireRate;
    }

    void FixedUpdate()
    {   

        if ((Input.GetButtonDown("Fire1") && Time.time > nextfire) || (Time.time > nextfire && Input.GetButton("Fire1")))
            {
                Shoot();

                if (playerIsAlive)
                {
                    FindObjectOfType<AudioManager>().Play("Shoot");
                }

                nextfire = Time.time + firerate;
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

            // ! Test code
            // GameObject bullet = ObjectPool.instance.GetPooledObject();

            // if (bullet != null)
            //     {
            //         Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            //         // Reset velocity before applying force
            //         rb.velocity = Vector2.zero;

            //         // Apply force in the calculated direction
            //         rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);
            //         bullet.transform.rotation = bulletRotation; // Set rotation

            //         bullet.SetActive(true);
            //     }

            // ! End of test code

            GameObject bullet = Instantiate(bulletPreFab, firepoint.position, bulletRotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Star powerup"))
        {   
            FindObjectOfType<AudioManager>().Play("SuperGun");
            Destroy(collision.gameObject);
            StartCoroutine(ActivateStarPowerup());
        }
    }

    IEnumerator ActivateStarPowerup()
    {
        // Set the fire rate to the star powerup rate
        firerate = starPowerupFireRate;

        // Wait for 8 seconds
        yield return new WaitForSeconds(12f);

        // Reset the fire rate to the normal rate
        firerate = normalFireRate;
    }
}
