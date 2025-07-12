using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;
    [SerializeField] Animator sceneTransAnim;

    public delegate void SceneTransitionEvent(bool isTransitioning);
    public static event SceneTransitionEvent OnSceneTransition; // Add this line

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeAndLoad(sceneName));
    }

    private IEnumerator FadeAndLoad(string sceneName)
    {
        if (OnSceneTransition != null)
        {
            OnSceneTransition(true); // Transition started
        }

        sceneTransAnim.SetTrigger("End");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneName);
        sceneTransAnim.SetTrigger("Start");

        if (OnSceneTransition != null)
        {
            OnSceneTransition(false); // Transition ended
        }
    }
}