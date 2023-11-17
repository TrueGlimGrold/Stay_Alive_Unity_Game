using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{

    public Canvas mainMenuCanvas;
    public Canvas mapsCanvas;
    public GameObject player;

    // Make a map variable

    public string selectedMap = "Map1";

    [SerializeField]
    private Sprite[] mapImages;

    // Maps buttons

    [SerializeField]
    private Button btnMaps;

    [SerializeField]
    private Button btnUpgrades;

    [SerializeField]
    private Button btnSkins;

    [SerializeField]
    private Button btnQuit;

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

    // MainMenu map image and text

    [SerializeField]
    private Image mapImageDisplay;

    [SerializeField]
    private TextMeshProUGUI mapText;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("MenuMusic");

        btnMaps.onClick.AddListener(() => NavigateMainMenu(btnMaps));

        btnUpgrades.onClick.AddListener(() => NavigateMainMenu(btnUpgrades));

        btnUpgrades.onClick.AddListener(() => NavigateMainMenu(btnSkins));

        btnUpgrades.onClick.AddListener(() => NavigateMainMenu(btnQuit));

        // Add event listeners for level buttons as well

        navLevel1.onClick.AddListener(() => NavigateLevels(navLevel1, "Map1"));
        navLevel2.onClick.AddListener(() => NavigateLevels(navLevel2, "Map2"));
        navLevel3.onClick.AddListener(() => NavigateLevels(navLevel3, "Map3"));
        navLevel4.onClick.AddListener(() => NavigateLevels(navLevel4, "Map4"));
        navLevel5.onClick.AddListener(() => NavigateLevels(navLevel5, "Map5"));
        navLevel6.onClick.AddListener(() => NavigateLevels(navLevel6, "Map6"));

        mapText.text = "Graveyard";
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
        else if (selectedMap == "Map3")
        {
            SceneManager.LoadScene(2);
        }
        else if (selectedMap == "Map5")
        {
            SceneManager.LoadScene(3);
        }

        // Todo Still need to make maps 2,4, and 6

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
            // Add logic for BtnUpgrades.
        }
        else if (clickedButton.name == "BtnQuit") 
        {
            Application.Quit();
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
            
            mainMenuCanvas.gameObject.SetActive(true);
            player.gameObject.SetActive(true);
            mapsCanvas.gameObject.SetActive(false);

            mapImageDisplay.sprite = mapImages[0];
        }
        else if (clickedButton.name == "NavLevel2") // Customize for BtnUpgrades.
        {   
            FindObjectOfType<AudioManager>().Play("Shoot");

            // Set the selected map based on the mapName parameter
            selectedMap = mapName;

            // TODO Update value once you have made a map
            // we change the text mesh pro value dependant on which Navbutton was selected 
            mapText.text = "Orange";

            mainMenuCanvas.gameObject.SetActive(true);
            player.gameObject.SetActive(true);
            mapsCanvas.gameObject.SetActive(false);

        }
        else if (clickedButton.name == "NavLevel3") // Customize for BtnUpgrades.
        {   
            FindObjectOfType<AudioManager>().Play("Shoot");

            // Set the selected map based on the mapName parameter
            selectedMap = mapName;

            // we change the text mesh pro value dependant on which Navbutton was selected 
            mapText.text = "Laberinth";

            mainMenuCanvas.gameObject.SetActive(true);
            player.gameObject.SetActive(true);
            mapsCanvas.gameObject.SetActive(false);

            mapImageDisplay.sprite = mapImages[1];
        }
        else if (clickedButton.name == "NavLevel4") // Customize for BtnUpgrades.
        {   
            FindObjectOfType<AudioManager>().Play("Shoot");

            // Set the selected map based on the mapName parameter
            selectedMap = mapName;

            // TODO Update value once you have made a map
            // we change the text mesh pro value dependant on which Navbutton was selected 
            mapText.text = "Yellow";

            mainMenuCanvas.gameObject.SetActive(true);
            player.gameObject.SetActive(true);
            mapsCanvas.gameObject.SetActive(false);
        }
        else if (clickedButton.name == "NavLevel5") // Customize for BtnUpgrades.
        {   
            FindObjectOfType<AudioManager>().Play("Shoot");

            // Set the selected map based on the mapName parameter
            selectedMap = mapName;

            // we change the text mesh pro value dependant on which Navbutton was selected 
            mapText.text = "Colloseum";

            mainMenuCanvas.gameObject.SetActive(true);
            player.gameObject.SetActive(true);
            mapsCanvas.gameObject.SetActive(false);

            mapImageDisplay.sprite = mapImages[2];
        }
        else if (clickedButton.name == "NavLevel6") // Customize for BtnUpgrades.
        {   
            FindObjectOfType<AudioManager>().Play("Shoot");
            
            // Set the selected map based on the mapName parameter
            selectedMap = mapName;

            // TODO Update value once you have made a map
            // we change the text mesh pro value dependant on which Navbutton was selected 
            mapText.text = "Unknown";

            mainMenuCanvas.gameObject.SetActive(true);
            player.gameObject.SetActive(true);
            mapsCanvas.gameObject.SetActive(false);
        }
        // Add more conditions for other buttons as needed.
    }
}
