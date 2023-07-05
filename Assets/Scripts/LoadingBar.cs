using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoadingSceneSlider : MonoBehaviour
{
    [SerializeField] private string targetSceneName; // The name of the target scene to load

    private void Start()
    {
        StartCoroutine(LoadTargetScene());
    }

    private IEnumerator LoadTargetScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetSceneName);
        asyncLoad.allowSceneActivation = false; // Prevent scene from automatically activating

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // Normalize the progress
            GetComponent<Slider>().value = progress;

            if (progress >= 1f)
            {
                asyncLoad.allowSceneActivation = true; // Allow scene activation when progress is complete
            }

            yield return null;
        }
    }
}