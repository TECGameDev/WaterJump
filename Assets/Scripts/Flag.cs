using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour {
    // Add a public field for the scene name
    public string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            // Load the specified scene when the player reaches the flag
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
