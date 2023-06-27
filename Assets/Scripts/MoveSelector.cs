using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorMovement : MonoBehaviour
{
    public Transform boardParent; // Reference to the parent object (Board) that holds the child objects

    private Transform[] boardCells; // Array to store references to the child objects

    private int currentCellIndex; // Index of the current cell the selector is on

    private bool isSelecting; // Flag to indicate if the selector is in selection mode
    private Transform selectedObject; // Reference to the selected object

    private void Start()
    {
        // Get all child objects of the board and store their references
        boardCells = new Transform[boardParent.childCount];
        for (int i = 0; i < boardParent.childCount; i++)
        {
            boardCells[i] = boardParent.GetChild(i);
        }

        // Set the initial cell index to 0 (first child object)
        currentCellIndex = 0;

        // Move the selector to the initial cell
        MoveSelectorToCurrentCell();
    }

    private void Update()
    {
        if (isSelecting)
        {
            // Check for input to move the selected object
            if (Input.GetKeyDown(KeyCode.W))
            {
                MoveSelectedObject(0, 1); // Move up
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                MoveSelectedObject(0, -1); // Move down
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                MoveSelectedObject(-1, 0); // Move left
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                MoveSelectedObject(1, 0); // Move right
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                PlaceObject(); // Place the object back down
            }
        }
        else
        {
            // Check for input to move the selector
            if (Input.GetKeyDown(KeyCode.W))
            {
                MoveSelector(8); // Move up by 8 cells (invert of -8)
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                MoveSelector(-8); // Move down by 8 cells (invert of 8)
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                MoveSelector(-1); // Move left by 1 cell
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                MoveSelector(1); // Move right by 1 cell
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                SelectObject(); // Select the object
            }
        }
    }

    private void MoveSelector(int cellOffset)
    {
        // Calculate the new cell index
        int newCellIndex = currentCellIndex + cellOffset;

        // Check if the new cell index is within the valid range
        if (newCellIndex >= 0 && newCellIndex < boardCells.Length)
        {
            // Update the current cell index
            currentCellIndex = newCellIndex;

            // Move the selector to the new cell position while preserving the Z coordinate
            MoveSelectorToCurrentCell();
        }
    }

    private void MoveSelectorToCurrentCell()
    {
        // Get the position of the current cell
        Vector3 cellPosition = boardCells[currentCellIndex].position;

        // Preserve the Z coordinate of the selector
        cellPosition.z = transform.position.z;

        // Move the selector to the new cell position
        transform.position = cellPosition;
    }

    private void SelectObject()
    {
        // Check if there is an object in the current cell
        Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Selectable"))
            {
                // Select the object
                isSelecting = true;
                selectedObject = collider.transform;
                break;
            }
        }
    }

    private void MoveSelectedObject(int xOffset, int yOffset)
    {
        // Calculate the target cell index
        int targetCellIndex = currentCellIndex + xOffset + (yOffset * 8);

        // Check if the target cell index is within the valid range and is empty
        if (targetCellIndex >= 0 && targetCellIndex < boardCells.Length && !IsCellOccupied(targetCellIndex))
        {
            // Move the selected object to the target cell position
            Vector3 targetPosition = boardCells[targetCellIndex].position;
            targetPosition.z = selectedObject.position.z; // Preserve the Z coordinate
            selectedObject.position = targetPosition;

            // Update the current cell index
            currentCellIndex = targetCellIndex;

            // Move the selector to the new cell position
            MoveSelectorToCurrentCell();
        }
    }

    private bool IsCellOccupied(int cellIndex)
    {
        // Check if there is an object in the specified cell
        Collider2D[] colliders = Physics2D.OverlapPointAll(boardCells[cellIndex].position);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Selectable"))
            {
                return true;
            }
        }
        return false;
    }

    private void PlaceObject()
    {
        // Check if there is a selected object
        if (selectedObject != null)
        {
            // Place the object back down in the current cell
            Vector3 targetPosition = boardCells[currentCellIndex].position;
            targetPosition.z = selectedObject.position.z; // Preserve the Z coordinate
            selectedObject.position = targetPosition;

            // Clear the selected object and exit selection mode
            selectedObject = null;
            isSelecting = false;
        }
    }
}