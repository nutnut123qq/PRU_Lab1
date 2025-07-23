using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioListenerManager : MonoBehaviour
{
    public static AudioListenerManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            
            EnsureSingleAudioListener();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        EnsureSingleAudioListener();
    }

    public static void EnsureSingleAudioListener()
    {
        AudioListener[] listeners = FindObjectsByType<AudioListener>(FindObjectsSortMode.None);

        if (listeners.Length > 1)
        {
            AudioListener keepListener = null;
            Camera mainCamera = Camera.main;

            if (mainCamera != null && mainCamera.GetComponent<AudioListener>() != null)
            {
                keepListener = mainCamera.GetComponent<AudioListener>();
            }
            else
            {
                keepListener = listeners[0];
            }

            for (int i = 0; i < listeners.Length; i++)
            {
                if (listeners[i] != keepListener)
                {
                    Destroy(listeners[i]);
                }
            }
        }
        else if (listeners.Length == 0)
        {
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                mainCamera.gameObject.AddComponent<AudioListener>();
            }
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        if (Instance == null)
        {
            GameObject audioListenerManager = new GameObject("AudioListenerManager");
            audioListenerManager.AddComponent<AudioListenerManager>();
        }
    }
}
