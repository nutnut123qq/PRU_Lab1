using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class AudioListenerChecker
{
    static AudioListenerChecker()
    {
        EditorSceneManager.sceneOpened += OnSceneOpened;
        EditorApplication.hierarchyChanged += OnHierarchyChanged;
    }

    private static void OnSceneOpened(UnityEngine.SceneManagement.Scene scene, OpenSceneMode mode)
    {
        CheckAudioListeners();
    }

    private static void OnHierarchyChanged()
    {
        CheckAudioListeners();
    }

    [MenuItem("Tools/Audio/Kiểm tra AudioListener")]
    public static void CheckAudioListeners()
    {
        if (!Application.isPlaying)
        {
            AudioListener[] listeners = Object.FindObjectsByType<AudioListener>(FindObjectsSortMode.None);

            if (listeners.Length > 1)
            {
                for (int i = 0; i < listeners.Length; i++)
                {
                    Debug.Log($"AudioListener {i + 1}: {listeners[i].gameObject.name}", listeners[i].gameObject);
                }
            }
        }
    }

    [MenuItem("Tools/Audio/Sửa AudioListener tự động")]
    public static void FixAudioListeners()
    {
        if (!Application.isPlaying)
        {
            AudioListener[] listeners = Object.FindObjectsByType<AudioListener>(FindObjectsSortMode.None);

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
                        Object.DestroyImmediate(listeners[i]);
                    }
                }

                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
            else if (listeners.Length == 0)
            {
                Camera mainCamera = Camera.main;
                if (mainCamera != null)
                {
                    mainCamera.gameObject.AddComponent<AudioListener>();
                    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                }
            }
        }
    }

    private static string GetGameObjectPath(GameObject obj)
    {
        string path = obj.name;
        Transform parent = obj.transform.parent;
        
        while (parent != null)
        {
            path = parent.name + "/" + path;
            parent = parent.parent;
        }
        
        return path;
    }
}
