using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private int currentAttack;

    public int CurrentAttack { get { return currentAttack; } }

    public void SetAttack(int startingAttack)
    {
        currentAttack = startingAttack;
    }

    public void IncreaseAttack(int amount)
    {
        currentAttack += amount;
    }

    public void DecreaseAttack(int amount)
    {
        currentAttack -= amount;
        if (currentAttack < 0)
            currentAttack = 0;
    }
}