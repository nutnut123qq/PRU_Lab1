using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinemachineCameraManager : MonoBehaviour
{
    private CinemachineCamera cinemachineCam;
    public static CinemachineCameraManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this manager persistent
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        FindCinemachineCamera();
    }
    private void Update()
    {
        /*if (SceneManager.GetActiveScene().name.Equals("EndMenu"))
        {
            gameObject.SetActive(false);
            return;
        }*/
    }
    private void FindCinemachineCamera()
    {
        cinemachineCam = FindObjectsByType<CinemachineCamera>(FindObjectsSortMode.None).FirstOrDefault();

        if (cinemachineCam != null)
        {
            // Disable extra AudioListeners
            AudioListener[] listeners = FindObjectsByType<AudioListener>(FindObjectsSortMode.None);
            if (listeners.Length > 1)
            {
                for (int i = 1; i < listeners.Length; i++)
                {
                    Destroy(listeners[i]); // Destroy extra listeners
                }
            }
        }
    }

    public void SetFollowTarget(Transform target)
    {
        if (cinemachineCam == null)
        {
            FindCinemachineCamera(); // Try finding it again
        }

        if (cinemachineCam != null)
        {
            cinemachineCam.Follow = target;
        }
        else
        {
            Debug.LogWarning("CinemachineCamera not found in the scene!");
        }
    }
}
