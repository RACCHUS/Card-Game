using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsButton : MonoBehaviour
{
    public string settingsSceneName; // Name of the settings scene to load

    private void Start()
    {
        // Attach a click event listener to the button
        GetComponent<TMPro.TextMeshProUGUI>().GetComponent<UnityEngine.UI.Button>().onClick.AddListener(LoadSettingsScene);
    }

    private void LoadSettingsScene()
    {
        SceneManager.LoadScene(settingsSceneName);
    }
}