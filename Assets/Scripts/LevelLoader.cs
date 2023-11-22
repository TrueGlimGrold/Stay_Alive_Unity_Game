using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelLoader : MonoBehaviour
{

    public Canvas mainMenuCanvas;
    public Canvas mapsCanvas;
    public Canvas upgradesCanvas;
    public GameObject player;

    // Make a map variable

    public string selectedMap = "Map5";

    [SerializeField]
    private Sprite[] mapImages;

    // Maps buttons

    [SerializeField]
    private Button btnMaps;

    [SerializeField]
    private Button btnUpgrades;

    [SerializeField]
    private Button btnQuit;

    [SerializeField]
    private Button btnReset;

    // Level Nav buttons here

    [SerializeField]
    private Button navLevel1;

    [SerializeField]
    private Button navLevel2;

    [SerializeField]
    private Button navLevel3;

    [SerializeField]
    private Button navLevel4;

    [SerializeField]
    private Button navLevel5;

    [SerializeField]
    private Button navLevel6;

    // Getting and displaying different scores. 
    [SerializeField]
    private TextMeshProUGUI score1; 

    [SerializeField]
    private TextMeshProUGUI score2;

    [SerializeField]
    private TextMeshProUGUI score3;

    [SerializeField]
    private TextMeshProUGUI score4; 

    [SerializeField]
    private TextMeshProUGUI score5;

    [SerializeField]
    private TextMeshProUGUI score6;

    [SerializeField]
    private TextMeshProUGUI mainScore;

    // MainMenu map image and text

    [SerializeField]
    private Image mapImageDisplay;

    [SerializeField]
    private TextMeshProUGUI mapText;

    Color cyan = new Color(0, 0.9568627f, 1f, 1f);

    Color violet = new Color32(255, 0, 254, 255);

    Color green = new Color(0, 1f, 0.0627451f, 1f); 

    // Adding in the variables and lists for the point system

    [SerializeField]
    private TextMeshProUGUI skillPointsText;

    [SerializeField]
    private TextMeshProUGUI scoreNeededText;

    [SerializeField]
    private Image speedDisplay;

    [SerializeField]
    private Image fireRateDisplay;

    [SerializeField]
    private Sprite[] speedImages;

    [SerializeField]
    private Sprite[] fireRateImages;

    [SerializeField]
    private Button btnSpeed;

    [SerializeField]
    private Button btnFireRate;

    [SerializeField]
    private Button mainMenuBtn;

    private int speedLevel = 0;

    private int fireRateLevel = 0;

    List<int> levelRequirements = new List<int>
    {10000, 30000, 50000, 70000, 90000, 120000, 150000, 200000, 250000, 320000, 
    400000, 550000, 700000, 900000, 1100000, 1400000};

    private int totalScore;
    int skillPointsSpent;
    private int currentLevel;

    private void Start()
    {   
        // Set the game to fullscreen
        Screen.fullScreen = true;

        // Set the resolution to fit the screen
        int screenWidth = Screen.currentResolution.width;
        int screenHeight = Screen.currentResolution.height;
        Screen.SetResolution(screenWidth, screenHeight, Screen.fullScreen);

        FindObjectOfType<AudioManager>().Play("MenuMusic");

        btnMaps.onClick.AddListener(() => NavigateMainMenu(btnMaps));

        btnUpgrades.onClick.AddListener(() => NavigateMainMenu(btnUpgrades));

        btnQuit.onClick.AddListener(() => NavigateMainMenu(btnQuit));

        btnReset.onClick.AddListener(() => NavigateMainMenu(btnReset));

        // Add event listeners for level buttons as well

        navLevel1.onClick.AddListener(() => NavigateLevels(navLevel1, "Map1"));
        navLevel2.onClick.AddListener(() => NavigateLevels(navLevel2, "Map2"));
        navLevel3.onClick.AddListener(() => NavigateLevels(navLevel3, "Map3"));
        navLevel4.onClick.AddListener(() => NavigateLevels(navLevel4, "Map4"));
        navLevel5.onClick.AddListener(() => NavigateLevels(navLevel5, "Map5"));
        navLevel6.onClick.AddListener(() => NavigateLevels(navLevel6, "Map6"));

        mapText.text = "The Colloseum";

        int sceneScore = PlayerPrefs.GetInt("Scene5Score", 0);

        // Updating score values 
        // Load scores for each scene and update TextMeshPro texts
        for (int i = 1; i <= 6; i++)
        {
            sceneScore = PlayerPrefs.GetInt("Scene" + i + "Score", 0);

            // Update the corresponding TextMeshPro text
            switch (i)
            {
                case 1:
                    score1.text = "Score: " + sceneScore.ToString();
                    totalScore += sceneScore;
                    break;
                case 2:
                    score2.text = "Score: " + sceneScore.ToString();
                    totalScore += sceneScore;
                    break;
                case 3:
                    score3.text = "Score: " + sceneScore.ToString();
                    totalScore += sceneScore;
                    break;
                case 4:
                    score4.text = "Score: " + sceneScore.ToString();
                    totalScore += sceneScore;
                    break;
                case 5:
                    score5.text = "Score: " + sceneScore.ToString();
                    totalScore += sceneScore;
                    break;
                case 6:
                    score6.text = "Score: " + sceneScore.ToString();
                    totalScore += sceneScore;
                    break;
                // Add more cases if you have more scenes
            }
        }

        // Get the skill points spent 
        skillPointsSpent = PlayerPrefs.GetInt("skillSpent");

        // Load the high score for the selected level
        sceneScore = PlayerPrefs.GetInt("Scene5Score", 0);

        // Update mainScore with the high score of the selected level
        mainScore.text = "High Score: " + sceneScore.ToString();

        // Load in the current value for both skill sprites
        speedLevel = PlayerPrefs.GetInt("speed");
        ChangeSprite(speedLevel, "speed");

        fireRateLevel = PlayerPrefs.GetInt("fireRate");
        ChangeSprite(fireRateLevel, "fireRate");

        // Display skill points, and score needed for next level.
        currentLevel = CalculateCurrentLevel();

        // Calculate current skill points available
        int currentSkillPoints = currentLevel - skillPointsSpent;
        skillPointsText.text = "Skill Points: " + currentSkillPoints.ToString();

         // Calculate score needed for the next level
        int scoreNeeded = (currentLevel < levelRequirements.Count) ? levelRequirements[currentLevel] - totalScore : 0;
        scoreNeededText.text = "Score Needed: " + scoreNeeded.ToString();

        Debug.Log("Current Level: " + currentLevel);
        Debug.Log("Skill Points Spent: " + skillPointsSpent);
        Debug.Log("Current Skill Points: " + currentSkillPoints);
        Debug.Log("Total Score: " + totalScore);
    }

    public void LoadSceneAfterAudio()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();

        // Play the audio
        audioManager.Play("Start");

        // Get the length of the audio clip
        float audioLength = audioManager.GetClipLength("Start");

        // Delay the scene loading by the length of the audio clip
        Invoke("LoadNextScene", audioLength);
    }

    void LoadNextScene()
    {   if (selectedMap == "Map1")
        {
            SceneManager.LoadScene(1);
        } 
        else if (selectedMap == "Map2")
        {
            SceneManager.LoadScene(2);
        }
        else if (selectedMap == "Map3")
        {
            SceneManager.LoadScene(3);
        }
        else if (selectedMap == "Map4")
        {
            SceneManager.LoadScene(4);
        }
        else if (selectedMap == "Map5")
        {
            SceneManager.LoadScene(5);
        }
        else if (selectedMap == "Map6")
        {
            SceneManager.LoadScene(6);
        }
    }


    public void NavigateMainMenu(Button clickedButton)
    {
        // Assuming you have assigned the Canvas objects in the Unity Editor.
        if (clickedButton.name == "BtnMaps") // Customize based on button name or other criteria.
        {
            
            mainMenuCanvas.gameObject.SetActive(false);
            
            player.gameObject.SetActive(false);
            
            mapsCanvas.gameObject.SetActive(true);
            
        }
        else if (clickedButton.name == "BtnUpgrades") // Customize for BtnUpgrades.
        {   
            FindObjectOfType<AudioManager>().Play("Shoot");

            mainMenuCanvas.gameObject.SetActive(false);
            
            player.gameObject.SetActive(false);
            
            upgradesCanvas.gameObject.SetActive(true);
        }
        else if (clickedButton.name == "BtnQuit") 
        {   
            // ! Find a way to get this sound to play
            FindObjectOfType<AudioManager>().Play("Shoot");

            Application.Quit();
        }
        else if (clickedButton.name == "BtnMenu")
        {   
            FindObjectOfType<AudioManager>().Play("Shoot");

            mainMenuCanvas.gameObject.SetActive(true);
            
            player.gameObject.SetActive(true);
            
            upgradesCanvas.gameObject.SetActive(false);
        }

        else if (clickedButton.name == "BtnReset")
        {   
            FindObjectOfType<AudioManager>().Play("Shoot");

            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(0);
            Debug.Log("All PlayerPrefs data has been reset.");
        }
    }

    public void NavigateLevels(Button clickedButton, string mapName)
    {
        // Assuming you have assigned the Canvas objects in the Unity Editor.
        if (clickedButton.name == "NavLevel1") // Customize based on button name or other criteria.
        {   
            FindObjectOfType<AudioManager>().Play("Shoot");

            // Set the selected map based on the mapName parameter
            selectedMap = mapName;

            // we change the text mesh pro value dependant on which Navbutton was selected 
            mapText.text = "Graveyard";
            mapText.color = green;
            
            mainMenuCanvas.gameObject.SetActive(true);
            player.gameObject.SetActive(true);
            mapsCanvas.gameObject.SetActive(false);

            mapImageDisplay.sprite = mapImages[0];

            // Load the high score for the selected level
            int sceneScore = PlayerPrefs.GetInt("Scene1Score", 0);

            // Update mainScore with the high score of the selected level
            mainScore.text = "High Score: " + sceneScore.ToString();
        }
        else if (clickedButton.name == "NavLevel2") // Customize for BtnUpgrades.
        {   
            FindObjectOfType<AudioManager>().Play("Shoot");

            // Set the selected map based on the mapName parameter
            selectedMap = mapName;

            // we change the text mesh pro value dependant on which Navbutton was selected 
            mapText.text = "Slow Death";
            mapText.color = cyan;

            mainMenuCanvas.gameObject.SetActive(true);
            player.gameObject.SetActive(true);
            mapsCanvas.gameObject.SetActive(false);

            mapImageDisplay.sprite = mapImages[1];

            // Load the high score for the selected level
            int sceneScore = PlayerPrefs.GetInt("Scene2Score", 0);

            // Update mainScore with the high score of the selected level
            mainScore.text = "High Score: " + sceneScore.ToString();

        }
        else if (clickedButton.name == "NavLevel3") // Customize for BtnUpgrades.
        {   
            FindObjectOfType<AudioManager>().Play("Shoot");

            // Set the selected map based on the mapName parameter
            selectedMap = mapName;

            // we change the text mesh pro value dependant on which Navbutton was selected 
            mapText.text = "The Labyrinth";
            mapText.color = violet;

            mainMenuCanvas.gameObject.SetActive(true);
            player.gameObject.SetActive(true);
            mapsCanvas.gameObject.SetActive(false);

            mapImageDisplay.sprite = mapImages[2];

            // Load the high score for the selected level
            int sceneScore = PlayerPrefs.GetInt("Scene3Score", 0);

            // Update mainScore with the high score of the selected level
            mainScore.text = "High Score: " + sceneScore.ToString();
        }
        else if (clickedButton.name == "NavLevel4") // Customize for BtnUpgrades.
        {   
            FindObjectOfType<AudioManager>().Play("Shoot");

            // Set the selected map based on the mapName parameter
            selectedMap = mapName;

            // we change the text mesh pro value dependant on which Navbutton was selected 
            mapText.text = "The Factory";
            mapText.color = green;

            mainMenuCanvas.gameObject.SetActive(true);
            player.gameObject.SetActive(true);
            mapsCanvas.gameObject.SetActive(false);

            mapImageDisplay.sprite = mapImages[3];

            // Load the high score for the selected level
            int sceneScore = PlayerPrefs.GetInt("Scene4Score", 0);

            // Update mainScore with the high score of the selected level
            mainScore.text = "High Score: " + sceneScore.ToString();
        }
        else if (clickedButton.name == "NavLevel5") // Customize for BtnUpgrades.
        {   
            FindObjectOfType<AudioManager>().Play("Shoot");

            // Set the selected map based on the mapName parameter
            selectedMap = mapName;

            // we change the text mesh pro value dependant on which Navbutton was selected 
            mapText.text = "The Colloseum";
            mapText.color = cyan;

            mainMenuCanvas.gameObject.SetActive(true);
            player.gameObject.SetActive(true);
            mapsCanvas.gameObject.SetActive(false);

            mapImageDisplay.sprite = mapImages[4];

            // Load the high score for the selected level
            int sceneScore = PlayerPrefs.GetInt("Scene5Score", 0);

            // Update mainScore with the high score of the selected level
            mainScore.text = "High Score: " + sceneScore.ToString();
        }
        else if (clickedButton.name == "NavLevel6") // Customize for BtnUpgrades.
        {   
            FindObjectOfType<AudioManager>().Play("Shoot");
            
            // Set the selected map based on the mapName parameter
            selectedMap = mapName;

            // we change the text mesh pro value dependant on which Navbutton was selected 
            mapText.text = "Neon Hell";
            mapText.color = violet;

            mainMenuCanvas.gameObject.SetActive(true);
            player.gameObject.SetActive(true);
            mapsCanvas.gameObject.SetActive(false);

            mapImageDisplay.sprite = mapImages[5];

            // Load the high score for the selected level
            int sceneScore = PlayerPrefs.GetInt("Scene6Score", 0);

            // Update mainScore with the high score of the selected level
            mainScore.text = "High Score: " + sceneScore.ToString();
        }
        // Add more conditions for other buttons as needed.
    }

    void ChangeSprite(int points, string skillType)
    {   
        if (skillType == "speed" && points >= 0 && points < speedImages.Length)
        {
            speedDisplay.sprite = speedImages[points];
        } 
        else if (skillType == "fireRate" && points >= 0 && points < speedImages.Length)
        {
            fireRateDisplay.sprite = fireRateImages[points];
        }
    }

    public void IncreaseScore(Button clickedButton)
    {   
        if (skillPointsSpent < currentLevel){
            if (clickedButton.name == "BtnFireRate" && fireRateLevel < fireRateImages.Length -1) 
            {   
                fireRateLevel += 1;
                PlayerPrefs.SetInt("fireRate", fireRateLevel);
                FindObjectOfType<AudioManager>().Play("Upgrade");
                ChangeSprite(fireRateLevel, "fireRate");
            }
            else if (clickedButton.name == "BtnSpeed" && speedLevel < speedImages.Length -1) 
            {   
                speedLevel += 1;
                PlayerPrefs.SetInt("speed", speedLevel);
                FindObjectOfType<AudioManager>().Play("Upgrade");
                ChangeSprite(speedLevel, "speed");
            }

            skillPointsSpent += 1;

            // Calculate current skill points available
            int currentSkillPoints = currentLevel - skillPointsSpent;
            skillPointsText.text = "Skill Points: " + currentSkillPoints.ToString();

            // Calculate score needed for the next level
            int scoreNeeded = (currentLevel < levelRequirements.Count) ? levelRequirements[currentLevel] - totalScore : 0;
            scoreNeededText.text = "Score Needed: " + scoreNeeded.ToString();

            PlayerPrefs.SetInt("skillSpent", skillPointsSpent);
            }
        else if (skillPointsSpent >= currentLevel){
            if (clickedButton.name == "BtnFireRate") 
            {
                FindObjectOfType<AudioManager>().Play("Unavailable"); 
            }
            else if (clickedButton.name == "BtnSpeed") 
            {   
                FindObjectOfType<AudioManager>().Play("Unavailable"); 
            }
            }
    }

    private int CalculateCurrentLevel()
    {
        for (int i = 0; i < levelRequirements.Count; i++)
        {
            if (totalScore < levelRequirements[i])
            {
                return i; // Levels are usually 1-indexed, so add 1
            }
        }
        return levelRequirements.Count; // If the totalScore exceeds all level requirements
    }
}
