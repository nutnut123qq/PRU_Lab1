using spaceExplorer.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;  // To manage scenes
using UnityEngine.UI; // Optional, if you want to display the countdown on UI

public class TimerScript : MonoBehaviour
{
    // Time in seconds
    public float timeLimit = 10f;
    private float currentTime;
    public Text timerText;  // Reference to the Text UI element
    public PlayerEx playerEx;
    private bool isTimerActive = false;

    //public static TimerScript Instance { get; private set; } // Add this line

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        playerEx = Player.Instance.gameObject.GetComponent<PlayerEx>();
        ResetTimer();

        playerEx.OnDeath += PlayerEx_OnDeath;

        //if (Instance == null) // Check if instance exists
        //{
        //    Instance = this; // Set instance
        //    DontDestroyOnLoad(gameObject); // Keep it persistent
        //}
        //else
        //{
        //    Destroy(gameObject); // Destroy duplicate
        //    return; // Exit the Start method
        //}

        playerEx = Player.Instance.gameObject.GetComponent<PlayerEx>();
        ResetTimer();

        playerEx.OnDeath += PlayerEx_OnDeath;
    }

    private void PlayerEx_OnDeath(object sender, System.EventArgs e)
    {
        SceneTransition.instance.FadeToScene("EndMenu");
        //StartCoroutine(LoadEndMenuAfterDelay(2f));
    }
    private IEnumerator LoadEndMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneTransition.instance.FadeToScene("EndMenu");
        timerText.enabled = false;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "EndMenu" && SceneManager.GetActiveScene().name != "MainMenu")
        {
            timerText = GameObject.Find("TimerText").GetComponent<Text>();
        }

        if (isTimerActive)
        {
            currentTime -= Time.deltaTime;

            if (timerText != null)
            {
                // Update the timer text in the top-right of the screen
                timerText.text = "Time left: " + Mathf.Round(currentTime).ToString() + "s";
            }

            if (currentTime <= 0)
            {
                GoToNextScene();
            }
        }
    }

    void GoToNextScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        isTimerActive = false;

        // Check for the next scene and load it
        if (currentSceneName == "1-1")
        {
            SceneTransition.instance.FadeToScene("1-2");  // Transition from 1-1 to 1-2
        }
        else if (currentSceneName == "1-2")
        {
            SceneTransition.instance.FadeToScene("1-3");  // Transition from 1-2 to 1-3
        }
        else if (currentSceneName == "1-3")
        {
            SceneTransition.instance.FadeToScene("EndMenu");  // Transition from 1-3 to MainMenu or another scene
        }
    }

    void ResetTimer()
    {
        currentTime = timeLimit;  // Reset the timer for the new scene
        isTimerActive = true;  // Start the timer for the new scene
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetTimer();  // Reset the timer every time a new scene is loaded
    }
}
