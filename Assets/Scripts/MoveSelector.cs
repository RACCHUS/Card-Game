using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorMovement : MonoBehaviour
{
    public Transform boardParent; // Reference to the parent object (Board) that holds the child objects

    private Transform[] boardCells; // Array to store references to the child objects

    private int currentCellIndex; // Index of the current cell the selector is on

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
        transform.position = boardCells[currentCellIndex].position;
    }

    private void Update()
    {
        // Check for input and move the selector accordingly
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

            // Move the selector to the new cell position
            transform.position = boardCells[currentCellIndex].position;
        }
    }
}