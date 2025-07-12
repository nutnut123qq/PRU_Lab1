using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreTextAssigner : MonoBehaviour
{
    private void Awake()
    {
        AssignScoreText();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AssignScoreText();
    }

    private void AssignScoreText()
    {
        ScoreManager scoreManager = ScoreManager.instance;

        if (scoreManager != null)
        {
            Text scoreText = GameObject.Find("ScoreText").GetComponent<Text>();

            if (scoreText != null)
            {
                scoreManager.scoreText = scoreText;
                scoreManager.UpdateScoreText(); // Update the text here
            }
            else
            {
                Debug.LogError("ScoreText not found in scene: " + SceneManager.GetActiveScene().name);
            }
        }
        else
        {
            Debug.LogError("ScoreManager instance is null in scene: " + SceneManager.GetActiveScene().name);
        }
    }
}