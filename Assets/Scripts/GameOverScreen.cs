using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void MainMenuScene()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
