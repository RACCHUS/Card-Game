using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int currentHealth;

    public int CurrentHealth { get { return currentHealth; } }

    public void SetHealth(int startingHealth)
    {
        currentHealth = startingHealth;
    }

    public void IncreaseHealth(int amount)
    {
        currentHealth += amount;
    }

    public void DecreaseHealth(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
            currentHealth = 0;
    }
}