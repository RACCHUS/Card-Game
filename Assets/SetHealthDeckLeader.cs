using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object1Initializer : MonoBehaviour
{
    public GameObject object1;

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
}