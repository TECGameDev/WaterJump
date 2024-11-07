using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int lives = 3; // Player starts with 3 lives
    private Vector2 respawnPosition; // Position to respawn the player
    private GameObject player;
    private Vector2 initialWaterPosition; // Initial position for the Water
    private GameObject water; // Reference for the Water

    private void Awake()
    {
        // Persist between scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene load events
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeLevel(); // Initialize the player and respawn position
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Re-initialize the player and respawn position whenever a new scene is loaded
        InitializeLevel();
    }

    private void InitializeLevel()
    {
        // Find the player in the new scene
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            respawnPosition = player.transform.position;
        }
        else
        {
            Debug.LogWarning("Player object not found in scene!");
        }

        // Find and initialize the Water
        water = GameObject.Find("Water");
        if (water != null)
        {
            initialWaterPosition = water.transform.position;
        }
        else
        {
            Debug.LogWarning("Water object not found in scene!");
        }
    }

    public void RespawnPlayer()
    {
        if (lives > 1)
        {
            lives--;
            // Check if the player is still valid (especially after level change)
            if (player == null)
            {
                InitializeLevel();
            }
            // Move the player to the respawn position
            if (player != null)
            {
                player.transform.position = respawnPosition;
            }
            else
            {
                Debug.LogWarning("Player object not set for respawn!");
            }

            // Moves the water to its respawn position
            if(water != null)
            {
                water.transform.position = initialWaterPosition;
            }
        }
        else
        {
            // If no lives left, restart the game or go to Game Over screen
            SceneManager.LoadScene("Main Menu");
        }
    }

    public void SetRespawnPoint(Vector2 newRespawnPosition)
    {
        respawnPosition = newRespawnPosition;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from scene load events to avoid memory leaks
    }
}