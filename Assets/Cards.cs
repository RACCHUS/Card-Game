using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cards : MonoBehaviour
{
    public Transform contentTransform;
    public GameObject canvasPrefab;
    public TMP_Dropdown sortingDropdown; // Updated to TMP_Dropdown for TextMeshPro support

    public List<Card> cardList = new List<Card>();

    void Start()
    {
        // Create a new card
        Card Warhorse = new Card("Warhorse", 1000, "A horse that was used in battle.", Resources.Load<Sprite>("Cards/Warhorse"));
        cardList.Add(Warhorse);
        Card GoatFiend = new Card("Goat Fiend", 1000, "A creature that is particularly malicious.", Resources.Load<Sprite>("Cards/GoatFiend"));
        cardList.Add(GoatFiend);
        Card Commander = new Card("Commander", 2000, "The leader of a renowned army.", Resources.Load<Sprite>("Cards/Commander"));
        cardList.Add(Commander);
        Card Wrath = new Card("Wrath", 2500, "The sin of wrath.", Resources.Load<Sprite>("Cards/Wrath"));
        cardList.Add(Wrath);
        Card Warhorse1 = new Card("Warhorse1", 1000, "A horse that was used in battle.", Resources.Load<Sprite>("Cards/Warhorse"));
        cardList.Add(Warhorse1);
        Card GoatFiend1 = new Card("Goat Fiend1", 1000, "A creature that is particularly malicious.", Resources.Load<Sprite>("Cards/GoatFiend"));
        cardList.Add(GoatFiend1);
        Card Commander1 = new Card("Commander1", 2000, "The leader of a renowned army.", Resources.Load<Sprite>("Cards/Commander"));
        cardList.Add(Commander1);
        Card Wrath1 = new Card("Wrath1", 2500, "The sin of wrath.", Resources.Load<Sprite>("Cards/Wrath"));
        cardList.Add(Wrath1);
        // Add more cards to the cardList as needed...

        // Populate card slots in the content object
        foreach (Card card in cardList)
        {
            GameObject cardSlot = Instantiate(canvasPrefab, contentTransform);
            // Customize the card slot position, size, and other properties as needed

            // Retrieve the Image component from the child object called "Image"
            Image imageComponent = cardSlot.transform.Find("Image").GetComponent<Image>();
            if (imageComponent != null)
            {
                // Set the sprite of the Image component
                imageComponent.sprite = card.sprite;
            }
            else
            {
                Debug.LogError("Card slot prefab does not have an Image component or child object named 'Image'.");
            }

            // Retrieve the TextMeshPro objects for name and attack value
            TextMeshProUGUI nameText = cardSlot.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI attackText = cardSlot.transform.Find("Attack").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI descriptionText = cardSlot.transform.Find("Description").GetComponent<TextMeshProUGUI>();

            // Verify the TextMeshPro components
            if (nameText == null || attackText == null)
            {
                Debug.LogError("Card slot prefab does not have TextMeshPro components for name and attack value.");
            }
            else
            {
                // Assign the name and attack value to the TextMeshPro components
                nameText.text = card.name;
                descriptionText.text = card.description;
                attackText.text = card.attackValue.ToString();
            }

            // Optionally, access and modify other components in the card slot prefab

            // Activate the card slot (make it visible)
            cardSlot.SetActive(true);
        }

        // Add a listener to the dropdown's onValueChanged event
        sortingDropdown.onValueChanged.AddListener(OnSortingDropdownValueChanged);
    }

    void OnSortingDropdownValueChanged(int index)
    {
        // Get the selected sorting option from the dropdown
        string sortingOption = sortingDropdown.options[index].text;

        // Sort the cardList based on the selected sorting option
        switch (sortingOption)
        {
            case "None":
                // No sorting required, do nothing
                break;
            case "Name":
                cardList.Sort((a, b) => a.name.CompareTo(b.name));
                break;
            case "Attack":
                cardList.Sort((a, b) => a.attackValue.CompareTo(b.attackValue));
                break;
            default:
                Debug.LogError("Invalid sorting option selected.");
                break;
        }

        // Reorder the child objects based on the sorted cardList
        ReorderChildrenByProperty();
    }

    void ReorderChildrenByProperty()
    {
        // Clear the existing child objects
        foreach (Transform child in contentTransform)
        {
            Destroy(child.gameObject);
        }

        // Populate card slots in the content object based on the sorted cardList
        foreach (Card card in cardList)
        {
            GameObject cardSlot = Instantiate(canvasPrefab, contentTransform);
            // Customize the card slot position, size, and other properties as needed

            // Retrieve the Image component from the child object called "Image"
            Image imageComponent = cardSlot.transform.Find("Image").GetComponent<Image>();
            if (imageComponent != null)
            {
                // Set the sprite of the Image component
                imageComponent.sprite = card.sprite;
            }
            else
            {
                Debug.LogError("Card slot prefab does not have an Image component or child object named 'Image'.");
            }

            // Retrieve the TextMeshPro objects for name and attack value
            TextMeshProUGUI nameText = cardSlot.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI attackText = cardSlot.transform.Find("Attack").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI descriptionText = cardSlot.transform.Find("Description").GetComponent<TextMeshProUGUI>();

            // Verify the TextMeshPro components
            if (nameText == null || attackText == null)
            {
                Debug.LogError("Card slot prefab does not have TextMeshPro components for name and attack value.");
            }
            else
            {
                // Assign the name and attack value to the TextMeshPro components
                nameText.text = card.name;
                descriptionText.text = card.description;
                attackText.text = card.attackValue.ToString();
            }

            // Optionally, access and modify other components in the card slot prefab

            // Activate the card slot (make it visible)
            cardSlot.SetActive(true);
        }
    }
}