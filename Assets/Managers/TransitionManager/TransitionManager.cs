using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager intent;

    [SerializeField] private Animator transitionAnimator;

    private void Awake()
    {
        if (intent == null)
        {
            intent = this;
        }
        else
        {
            Destroy(gameObject); // Remove duplicatas.
        }
    }

    public void TransitionTo(string scene)
    {
        if (string.IsNullOrEmpty(scene))
        {
            Debug.LogError("Scene name is invalid!");
            return;
        }

        StartCoroutine(LoadLevel(scene));
    }

    private IEnumerator LoadLevel(string scene)
    {
        transitionAnimator.SetTrigger("Exit");

        AnimatorClipInfo[] clipInfo = transitionAnimator.GetCurrentAnimatorClipInfo(0);
        if (clipInfo.Length > 0)
        {
            yield return new WaitForSeconds(clipInfo[0].clip.length);
        }
        else
        {
            Debug.LogWarning("No animation clip found on the current animator state.");
            yield return new WaitForSeconds(1f); // Tempo padrão.
        }

        SceneManager.LoadScene(scene);
    }
}