using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject instructionsPanel;
    public GameObject mainUIPanel;

    public void PlayGame()
    {
        // Load the Gameplay scene
        SceneTransition.instance.FadeToScene("1-1");
    }

    public void ShowInstructions()
    {
        // Hide the main UI panel
        mainUIPanel.SetActive(false);

        // Show the instructions panel
        instructionsPanel.SetActive(true);
    }

    public void CloseInstructions()
    {
        // Hide the instructions panel
        instructionsPanel.SetActive(false);

        // Show the main UI panel
        mainUIPanel.SetActive(true);
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
        Debug.Log("Game Quit!"); // For testing in the editor
    }
}
