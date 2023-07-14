using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
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
                yield return new WaitForSeconds(3f); // Add a 3-second delay
                asyncLoad.allowSceneActivation = true; // Allow scene activation after the delay
            }
    
            yield return null;
        }
    }
}