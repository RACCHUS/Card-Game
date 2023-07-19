using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MenuOpener : MonoBehaviour
{
    public GameObject menuPrefab; // The prefab representing the menu
    private GameObject currentMenu; // Reference to the currently open menu
    public GameObject selector; // The Selector object
    public GameObject graveyardCanvas; // The Graveyard canvas
    public GameObject handCanvas; // The Hand canvas
    private GameObject objectToMove; // Reference to the object to move
    public GridGenerator gridGenerator; // Reference to the GridGenerator script

    private void OnMouseDown()
{
    if (Input.GetMouseButtonDown(0))
    {
        if (currentMenu != null)
        {
            // Close the current menu
            Destroy(currentMenu);
            currentMenu = null;

            // Re-enable the Selector object
            if (selector != null)
            {
                selector.SetActive(true);
            }

            return; // Exit the method to prevent opening a new menu
        }

        // Store a reference to the object that was clicked
        objectToMove = gameObject;

        // Instantiate the new menu prefab
        currentMenu = Instantiate(menuPrefab, transform.position, Quaternion.identity);

        // Attach a listener to the Move button
        Transform moveOptionTransform = currentMenu.transform.Find("MoveOption");
        if (moveOptionTransform != null)
        {
            Button moveOptionButton = moveOptionTransform.GetComponent<Button>();
            if (moveOptionButton != null)
            {
                moveOptionButton.onClick.AddListener(ShowMoveOptions);
            }
        }

        // Attach a listener to the Hand button
        Transform handButtonTransform = currentMenu.transform.Find("HandOption");
        if (handButtonTransform != null)
        {
            Button handButton = handButtonTransform.GetComponent<Button>();
            if (handButton != null)
            {
                handButton.onClick.AddListener(OpenHand);
            }
        }

        // Attach a listener to the CancelOption button
        Transform cancelOptionButtonTransform = currentMenu.transform.Find("CancelOption");
        if (cancelOptionButtonTransform != null)
        {
            Button cancelOptionButton = cancelOptionButtonTransform.GetComponent<Button>();
            if (cancelOptionButton != null)
            {
                cancelOptionButton.onClick.AddListener(CloseMenu);
            }
        }

        // Attach a listener to the Graveyard button
        Transform graveyardButtonTransform = currentMenu.transform.Find("GraveyardOption");
        if (graveyardButtonTransform != null)
        {
            Button graveyardButton = graveyardButtonTransform.GetComponent<Button>();
            if (graveyardButton != null)
            {
                graveyardButton.onClick.AddListener(OpenGraveyard);
            }
        }

        // Disable the Selector object
        if (selector != null)
        {
            selector.SetActive(false);
        }
    }
}

    private void CloseMenu()
    {
        if (currentMenu != null)
        {
            // Close the current menu
            Destroy(currentMenu);
            currentMenu = null;

            // Re-enable the Selector object
            if (selector != null)
            {
                selector.SetActive(true);
            }
        }    
    }

    private void OpenGraveyard()
    {
        // Enable the Graveyard canvas
        if (graveyardCanvas != null)
        {
            graveyardCanvas.SetActive(true);
        }

        // Close the current menu
        CloseMenu();

        // Disable the Selector object
        if (selector != null)
        {
            selector.SetActive(false);
        }
    }

    private void OpenHand()
    {
        // Enable the Graveyard canvas
        if (handCanvas != null)
        {
            handCanvas.SetActive(true);
        }

        // Close the current menu
        CloseMenu();

        // Disable the Selector object
        if (selector != null)
        {
            selector.SetActive(false);
        }
    }

    private void ShowMoveOptions()
    {
        // Close the current menu
        CloseMenu();

        // Disable the Selector object
        if (selector != null)
        {
            selector.SetActive(false);
        }

        // Get the square coordinates of the selected square
        ObjectPositioner objectPositioner = GetComponent<ObjectPositioner>();
        if (objectPositioner != null)
        {
            int x = objectPositioner.xCoordinate;
            int y = objectPositioner.yCoordinate;

            // Show available move options
            if (gridGenerator != null && objectToMove != null)
            {
                List<Vector2Int> availableMoves = new List<Vector2Int>();

                // Check if movement is allowed in each direction
                if (gridGenerator.MoveObject(objectToMove, x + 1, y))
                {
                    availableMoves.Add(new Vector2Int(x + 1, y));
                    gridGenerator.MoveObject(objectToMove, x, y); // Move back to original position
                }
                if (gridGenerator.MoveObject(objectToMove, x - 1, y))
                {
                    availableMoves.Add(new Vector2Int(x - 1, y));
                    gridGenerator.MoveObject(objectToMove, x, y); // Move back to original position
                }
                if (gridGenerator.MoveObject(objectToMove, x, y + 1))
                {
                    availableMoves.Add(new Vector2Int(x, y + 1));
                    gridGenerator.MoveObject(objectToMove, x, y); // Move back to original position
                }
                if (gridGenerator.MoveObject(objectToMove, x, y - 1))
                {
                    availableMoves.Add(new Vector2Int(x, y - 1));
                    gridGenerator.MoveObject(objectToMove, x, y); // Move back to original position
                }

                foreach (Vector2Int move in availableMoves)
                {
                    SquareController squareController = gridGenerator.GetSquareController(move.x, move.y);
                    if (squareController != null)
                    {
                        squareController.SetSelectable(true);
                        squareController.onSelected += () => MoveObject(move.x, move.y);
                    }
                }
            }
        }
    }

    private void MoveObject(int targetX, int targetY)
    {
        if (gridGenerator != null && objectToMove != null)
        {
            if (gridGenerator.MoveObject(objectToMove, targetX, targetY))
            {
                // Object moved successfully
    
                // Update the xCoordinate and yCoordinate fields of the ObjectPositioner script
                ObjectPositioner objectPositioner = objectToMove.GetComponent<ObjectPositioner>();
                if (objectPositioner != null)
                {
                    objectPositioner.xCoordinate = targetX;
                    objectPositioner.yCoordinate = targetY;
                }
    
                // Re-enable the Selector object
                if (selector != null)
                {
                    selector.SetActive(true);
                }
    
                // Remove onSelected events from all selectable squares
                SquareController[] squareControllers = FindObjectsOfType<SquareController>();
                foreach (SquareController squareController in squareControllers)
                {
                    if (squareController.IsSelectable())
                    {
                        squareController.RemoveAllOnSelectedHandlers();
                        squareController.SetSelectable(false);
                    }
                }
            }
            else
            {
                // Movement not allowed to the selected square
    
                // Re-enable the Selector object
                if (selector != null)
                {
                    selector.SetActive(true);
                }
            }
        }
    }
}