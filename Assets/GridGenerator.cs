using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject squarePrefab; // The prefab representing each square on the grid
    public int gridSize = 8; // The size of the grid

    private Vector3[,] squarePositions; // Array to store the positions of each square

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        squarePositions = new Vector3[gridSize, gridSize]; // Initialize the squarePositions array

        Vector3 boardPosition = transform.position; // Get the position of the Board object

        float squareSize = squarePrefab.GetComponent<SpriteRenderer>().bounds.size.x; // Get the size of the square prefab

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 spawnPosition = boardPosition + new Vector3(x * squareSize, y * squareSize, 0f); // Calculate the spawn position relative to the Board object
                GameObject squareObject = Instantiate(squarePrefab, spawnPosition, Quaternion.identity);
                squareObject.transform.parent = transform; // Parent the square object to the board object

                squarePositions[x, y] = squareObject.transform.position; // Store the position of the square

                SquareController squareController = squareObject.GetComponent<SquareController>();
                squareController.SetCoordinates(x + 1, y + 1);
            }
        }
    }

    public Vector3 GetSquarePosition(int x, int y)
    {
        if (x >= 1 && x <= gridSize && y >= 1 && y <= gridSize)
        {
            return squarePositions[x - 1, y - 1];
        }

        return Vector3.zero;
    }

    public bool MoveObject(GameObject objectToMove, int targetX, int targetY)
    {
        if (targetX >= 1 && targetX <= gridSize && targetY >= 1 && targetY <= gridSize)
        {
            Vector3 targetPosition = GetSquarePosition(targetX, targetY);
            if (targetPosition != Vector3.zero)
            {
                // Preserve the Z position of the object
                float zPosition = objectToMove.transform.position.z;
    
                // Set only the X and Y positions of the object
                objectToMove.transform.position = new Vector3(targetPosition.x, targetPosition.y, zPosition);
    
                return true; // Object moved successfully
            }
        }
    
        return false; // Movement not allowed to the selected square
    }

    public SquareController GetSquareController(int x, int y)
    {
        // Check if the coordinates are within the bounds of the grid
        if (x >= 1 && x <= gridSize && y >= 1 && y <= gridSize)
        {
            // Get the position of the square at the given coordinates
            Vector3 squarePosition = GetSquarePosition(x, y);

            // Find all SquareController objects in the scene
            SquareController[] squareControllers = FindObjectsOfType<SquareController>();

            // Loop through all SquareController objects
            foreach (SquareController squareController in squareControllers)
            {
                // Check if the position of this SquareController object matches the position of the square at the given coordinates
                if (squareController.transform.position == squarePosition)
                {
                    // Return this SquareController object
                    return squareController;
                }
            }
        }

        // No SquareController object was found at the given coordinates
        return null;
    }
}