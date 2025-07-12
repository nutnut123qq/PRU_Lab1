using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuManager : MonoBehaviour
{
    // Return to main menu scene
    public void EndGame()
    {
        SceneTransition.instance.FadeToScene("MainMenu");
        SceneManager.sceneLoaded += OnMainMenuLoaded; // Subscribe to the event
    }

    private void OnMainMenuLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            ScoreManager.instance.ResetScore();
            SceneManager.sceneLoaded -= OnMainMenuLoaded; // Unsubscribe from the event
        }
    }

    // Quit Game
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}
