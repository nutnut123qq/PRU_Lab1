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
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        FindCinemachineCamera();
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
