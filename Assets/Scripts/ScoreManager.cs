using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; 

    public int score = 0;
    public Text scoreText; // Reference to the UI Text element to display the score

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make ScoreManager persistent
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText(); // Update the UI
    }

}
