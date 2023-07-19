using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPositioner : MonoBehaviour
{
    public int xCoordinate; // The x-coordinate on the grid
    public int yCoordinate; // The y-coordinate on the grid

    public GridGenerator gridGenerator; // Reference to the GridGenerator script

    void Start()
    {
        if (gridGenerator != null)
        {
            // Get the position of the corresponding square on the grid
            Vector3 gridPosition = gridGenerator.GetSquarePosition(xCoordinate, yCoordinate);

            // Preserve the Z position
            gridPosition.z = transform.position.z;

            // Set the object's position to the calculated grid position
            transform.position = gridPosition;
        }
        else
        {
            Debug.LogWarning("GridGenerator reference not set on ObjectPositioner!");
        }
    }
}