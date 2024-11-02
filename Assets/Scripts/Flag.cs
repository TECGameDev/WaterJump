using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Load the Game Complete scene when the player reaches the flag
            SceneManager.LoadScene(2);
        }
    }
}
