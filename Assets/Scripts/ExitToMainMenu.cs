using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainMenuButton : MonoBehaviour
{
    public string mainMenuSceneName; // Name of the settings scene to load

    private void Start()
    {
        // Attach a click event listener to the button
        GetComponent<TMPro.TextMeshProUGUI>().GetComponent<UnityEngine.UI.Button>().onClick.AddListener(LoadMainMenuScene);
    }

    private void LoadMainMenuScene()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}