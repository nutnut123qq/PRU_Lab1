using UnityEngine;
using UnityEngine.SceneManagement;

public class Star : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Star collision detected with: " + collision.gameObject.name); // Check if collision occurs

        if (collision.CompareTag("Player"))
        {
            ScoreManager.instance.AddScore(1); // Increase the score
            Destroy(gameObject); // Remove the star
        }
    }
}
