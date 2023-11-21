using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Animator animator;
    private float spawnRate = 15f;
    [SerializeField] private GameObject[] enemyPrefabs;

    [HideInInspector]
    public bool canSpawn;
    [SerializeField] private float soundRadius = 10f;
    private AudioSource audioSource;

    private void Start()
    {   
        Debug.Log("Can the spawners spawn? 3" + canSpawn);
        audioSource = GetComponent<AudioSource>();
        canSpawn = true;
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds spawnTime = new WaitForSeconds(2f);
        WaitForSeconds after_spawn = new WaitForSeconds(0.4f);

        Debug.Log("Starting Spawner");

        // Initial spawn
        float distanceToListener = Vector2.Distance(transform.position, ListenerPosition());
        animator.SetBool("Is_Spawning", true);
        yield return spawnTime;

        if (audioSource != null && distanceToListener <= soundRadius)
        {
            audioSource.Play();
        }

        int rand = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyToSpawn = enemyPrefabs[rand];
        Instantiate(enemyToSpawn, transform.position, Quaternion.identity);

        yield return after_spawn;
        animator.SetBool("Is_Spawning", false);

        Debug.Log("Can the spawners spawn? 1: " + canSpawn);

        // Continue with regular spawning loop
        while (canSpawn)
        {
            Debug.Log("Can the spawners spawn? 2: " + canSpawn);
            yield return new WaitForSeconds(spawnRate);

            // Decrease spawn rate
            spawnRate = Mathf.Max(spawnRate * 0.90f, 0.01f);

            rand = Random.Range(0, enemyPrefabs.Length);

            distanceToListener = Vector2.Distance(transform.position, ListenerPosition());

            // Trigger the "Spawning" animation
            animator.SetBool("Is_Spawning", true);
            yield return spawnTime;

            // Trigger the spawning sound
            if (audioSource != null && distanceToListener <= soundRadius)
            {
                audioSource.Play();
            }

            enemyToSpawn = enemyPrefabs[rand];
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);

            yield return after_spawn;
            animator.SetBool("Is_Spawning", false);
        }

        Debug.Log("Exiting Spawner");
    }

    private Vector2 ListenerPosition()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            return mainCamera.transform.position;
        }
        else
        {
            return Vector2.zero; // Return a default position if the main camera is not found.
        }
    }

    public void StopSpawning()
    {   
        Debug.Log("StopSpawning called");
        canSpawn = false;
        StartCoroutine(DelayedStopSpawning());
    }

    private IEnumerator DelayedStopSpawning()
    {
        yield return null; // Wait for the next frame
        canSpawn = false;
    }
}
