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

    private void Start()
    {
        // Load the high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        highScoreText.text = "High Score: " + highScore.ToString();
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
            PlayerPrefs.SetInt(highScoreKey, highScore);
            PlayerPrefs.Save();
        }
    }

    public void resetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerHealth playerHealth = activePlayer.GetComponent<PlayerHealth>();
        playerHealth.SetPlayerIsAlive(true);
        FindObjectOfType<AudioManager>().Play("Music");
    }

    public void GameOver() {
        gameOverScreen.SetActive(true);
        FindObjectOfType<AudioManager>().Stop("Music");
    }
}