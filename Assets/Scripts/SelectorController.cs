using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorController : MonoBehaviour
{
    public Transform boardParent;
    public GameObject miniMenuCanvas;
    public GameObject selectorObject;

    internal Transform[] boardCells;
    internal int currentCellIndex;

    internal bool isSelecting;
    internal Transform selectedObject;

    public bool IsSelecting { get { return isSelecting; } }

    private void Start()
    {
        boardCells = new Transform[boardParent.childCount];
        for (int i = 0; i < boardParent.childCount; i++)
        {
            boardCells[i] = boardParent.GetChild(i);
        }

        currentCellIndex = 0;

        MoveSelectorToCurrentCell();
    }

    private void Update()
    {
        if (isSelecting)
        {
            HandleSelectionInput();
        }
        else
        {
            HandleSelectorInput();
        }
    }

    private void HandleSelectionInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveSelectedObject(0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveSelectedObject(0, -1);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveSelectedObject(-1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveSelectedObject(1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            PlaceObject();
        }
    }

    private void HandleSelectorInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveSelector(8);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveSelector(-8);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveSelector(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveSelector(1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            SelectObject();
        }
    }

    private void MoveSelector(int cellOffset)
    {
        int newCellIndex = currentCellIndex + cellOffset;

        if (newCellIndex >= 0 && newCellIndex < boardCells.Length)
        {
            currentCellIndex = newCellIndex;
            MoveSelectorToCurrentCell();
        }
    }

    public void MoveSelectorToCurrentCell()
    {
        Vector3 cellPosition = boardCells[currentCellIndex].position;
        cellPosition.z = transform.position.z;
        transform.position = cellPosition;
    }

    private void SelectObject()
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Selectable"))
            {
                isSelecting = true;
                selectedObject = collider.transform;
                break;
            }
        }
    }

    internal void MoveSelectedObject(int xOffset, int yOffset)
    {
        int targetCellIndex = currentCellIndex + xOffset + (yOffset * 8);

        if (targetCellIndex >= 0 && targetCellIndex < boardCells.Length && !IsCellOccupied(targetCellIndex))
        {
            Vector3 targetPosition = boardCells[targetCellIndex].position;
            targetPosition.z = selectedObject.position.z;
            selectedObject.position = targetPosition;

            currentCellIndex = targetCellIndex;
            MoveSelectorToCurrentCell();
        }
    }

    public bool IsCellOccupied(int cellIndex)
    {
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
        if (selectedObject != null)
        {
            Vector3 targetPosition = boardCells[currentCellIndex].position;
            targetPosition.z = selectedObject.position.z;
            selectedObject.position = targetPosition;

            selectedObject = null;
            isSelecting = false;

            miniMenuCanvas.SetActive(false);
        }
    }
}