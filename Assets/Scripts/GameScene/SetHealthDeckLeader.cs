using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Object1Initializer : MonoBehaviour
{
    public GameObject object1;
    public TextMeshProUGUI defeatText; // Reference to the TextMeshProUGUI component for displaying defeat

    private void Start()
    {
        Health health = object1.GetComponent<Health>();
        if (health != null)
        {
            health.SetHealth(100); // Example: set initial health of object1 to 100
            Debug.Log("Health set to 100 for object1.");
        }
        else
        {
            Debug.LogError("Health component not found on object1.");
        }
    }

    private void Update()
    {
        Health health = object1.GetComponent<Health>();
        if (health != null && health.CurrentHealth == 0)
        {
            DisplayDefeat();
        }
    }

    private void DisplayDefeat()
    {
        if (defeatText != null)
        {
            defeatText.text = "Defeat";
        }
        else
        {
            Debug.LogError("Defeat text component not found.");
        }
    }
}