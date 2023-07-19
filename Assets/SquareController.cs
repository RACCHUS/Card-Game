using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareController : MonoBehaviour
{
    public int xCoordinate; // The X coordinate of the square
    public int yCoordinate; // The Y coordinate of the square

    public event Action onSelected; // Event that is invoked when the square is selected

    private bool isSelectable = false; // Whether the square is selectable

    public void SetCoordinates(int x, int y)
    {
        xCoordinate = x;
        yCoordinate = y;
    }

    public void SetSelectable(bool value)
    {
        isSelectable = value;
    }

    public bool IsSelectable()
    {
        return isSelectable;
    }

    public void RemoveAllOnSelectedHandlers()
    {
        onSelected = null;
    }

    private void OnMouseDown()
    {
        if (isSelectable && onSelected != null)
        {
            onSelected.Invoke();
        }
    }
}