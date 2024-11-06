using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public int lives = 3; // Player starts with 3 lives
    private Vector2 respawnPosition; // Position to respawn the player
    private GameObject player;

    private void Awake() {
        // persist between scenes
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        // Set initial respawn point, e.g., starting point of the level
        player = GameObject.FindGameObjectWithTag("Player");
        respawnPosition = player.transform.position;
    }

    public void RespawnPlayer() {
        if (lives > 1) {
            lives--;
            // Move the player to the respawn position
            player.transform.position = respawnPosition;
        }
        else {
            // If no lives left, restart the game or go to Game Over screen
            SceneManager.LoadScene("Game Over");
        }
    }

    public void SetRespawnPoint(Vector2 newRespawnPosition) {
        respawnPosition = newRespawnPosition;
    }
}
