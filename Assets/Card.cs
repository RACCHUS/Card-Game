using UnityEngine;

public class Card
{
    public string name;
    public int attackValue;
    public string description;
    public Sprite sprite;

    public Card(string name, int attackValue, string description, Sprite sprite)
    {
        this.name = name;
        this.attackValue = attackValue;
        this.description = description;
        this.sprite = sprite;
    }

    public Card()
    {
        name = "Untitled Card";
        attackValue = 1;
        description = "This is a card.";
        sprite = null;
    }

    public override string ToString()
    {
        return $"Name: {name}, Attack Value: {attackValue}, Description: {description}";
    }
}