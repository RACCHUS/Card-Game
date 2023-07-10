using UnityEngine;
using TMPro;

public class CardCanvasUIHealth : MonoBehaviour
{
    private TextMeshProUGUI healthText;
    public GameObject object1; // Reference to the game object with Health component

    private void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();

        if (healthText == null)
        {
            Debug.LogError("healthText is not assigned.");
        }

        if (object1 == null)
        {
            Debug.LogError("object1 is not assigned.");
        }
        else
        {
            Health health = object1.GetComponent<Health>();
            if (health != null)
            {
                health.SetHealth(4000); // Example: set initial health of object1 to 100
                Debug.Log("Health set to 100 for object1.");
            }
            else
            {
                Debug.LogError("Health component not found on object1.");
            }
        }
    }

    private void Update()
    {
        if (healthText != null && object1 != null)
        {
            Health health = object1.GetComponent<Health>();
            if (health != null)
            {
                healthText.text = "" + health.CurrentHealth.ToString();
            }
            else
            {
                Debug.LogError("Health component not found on object1.");
            }
        }
    }
}