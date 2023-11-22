using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    [HideInInspector]
    public PlayerHealth playerIsAlive;
    public GameObject activePlayer;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject gameOverScreen;

    public int playerScore;
    public int highScore;

    // The PlayerPrefs key for the high score
    private string highScoreKey = "HighScore";

    public EnemySpawn enemySpawn;
    

    private void Start()
    {   
        // Set the game to fullscreen
        Screen.fullScreen = true;
        
        // Set the resolution to fit the screen
        int screenWidth = Screen.currentResolution.width;
        int screenHeight = Screen.currentResolution.height;
        Screen.SetResolution(screenWidth, screenHeight, Screen.fullScreen);

        // Load the high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt(highScoreKey + SceneManager.GetActiveScene().buildIndex, 0);
        highScoreText.text = "High Score: " + highScore.ToString();

        // Load the score for the current scene from PlayerPrefs
        playerScore = 0;
        scoreText.text = "Score: " + playerScore.ToString();

        FindObjectOfType<AudioManager>().Play("Music");

        // Find the EnemySpawn script in the scene and assign it to the variable
        enemySpawn = FindObjectOfType<EnemySpawn>();
    }

    // You can set the activePlayer reference when the player is spawned or activated.
    public void SetActivePlayer(GameObject player)
    {
        activePlayer = player;
    }

    public void addScore(int amountAdded) 
    {
        playerScore = playerScore + amountAdded;
        scoreText.text = "Score: " + playerScore.ToString();

        if (playerScore >= highScore)
        {
            highScore = playerScore;
            highScoreText.text = "High Score: " + highScore.ToString();

            // Save the updated high score to PlayerPrefs
            PlayerPrefs.SetInt(highScoreKey + SceneManager.GetActiveScene().buildIndex, highScore);
            PlayerPrefs.Save();

            // Save the updated score for the current scene to PlayerPrefs
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("Scene" + currentSceneIndex + "Score", playerScore);
            PlayerPrefs.Save();
        }
    }

    public void resetGame()
    {   
        // Reset player score and update UI
        playerScore = 0;
        scoreText.text = "Score: " + playerScore.ToString();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerHealth playerHealth = activePlayer.GetComponent<PlayerHealth>();
        playerHealth.SetPlayerIsAlive(true);
        FindObjectOfType<AudioManager>().Play("Music");
    }

    public void GameOver() {
        if (enemySpawn != null)
        {
            enemySpawn.StopSpawning();
        }
        
        gameOverScreen.SetActive(true);
        FindObjectOfType<AudioManager>().Stop("Music");
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
