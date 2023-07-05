using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIntializer : MonoBehaviour
{
    public GameObject object1;

    private void Start()
    {
        Attack attack = object1.GetComponent<Attack>();
        if (attack != null)
        {
            attack.SetAttack(1000); // Example: set initial health of object1 to 100
            Debug.Log("Attack set to 1000 for object1.");
        }
        else
        {
            Debug.LogError("Attack component not found on object1.");
        }
    }
}