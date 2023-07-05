using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSelector : MonoBehaviour
{
    public GameObject[] handCards;
    public RectTransform selectorRectTransform;

    private int selectedIndex = 0;

    private void Start()
    {
        SelectCard(selectedIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SelectPreviousCard();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            SelectNextCard();
        }

    }

    private void SelectPreviousCard()
    {
        DeselectCard(selectedIndex);
        selectedIndex--;
        if (selectedIndex < 0)
            selectedIndex = handCards.Length - 1;
        SelectCard(selectedIndex);
    }

    private void SelectNextCard()
    {
        DeselectCard(selectedIndex);
        selectedIndex++;
        if (selectedIndex >= handCards.Length)
            selectedIndex = 0;
        SelectCard(selectedIndex);
    }

    private void SelectCard(int index)
    {
        GameObject selectedCard = handCards[index];
        selectorRectTransform.position = selectedCard.transform.position;

        // Handle the appearance of the selected card (e.g., change color, scale, etc.)
        // ...
    }

    private void DeselectCard(int index)
    {
        // Handle the appearance of the deselected card (e.g., revert color, scale, etc.)
        // ...
    }

    private int GetCardIndex(GameObject cardObject)
    {
        for (int i = 0; i < handCards.Length; i++)
        {
            if (handCards[i] == cardObject)
                return i;
        }
        return -1;
    }
}