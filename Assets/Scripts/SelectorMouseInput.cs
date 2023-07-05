using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorMouseInput : MonoBehaviour
{
    public Transform boardParent;
    public GameObject miniMenuCanvas;
    public GameObject selectorObject;
    
    private SelectorController selectorController;
    private Transform[] boardCells;

    private void Start()
    {
        selectorController = GetComponent<SelectorController>();
        boardCells = new Transform[boardParent.childCount];
        for (int i = 0; i < boardParent.childCount; i++)
        {
            boardCells[i] = boardParent.GetChild(i);
        }
    }

    private void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                Transform clickedObject = hit.collider.transform;

                bool isBoardCell = false;
                for (int i = 0; i < boardCells.Length; i++)
                {
                    if (clickedObject == boardCells[i])
                    {
                        selectorController.currentCellIndex = i;
                        selectorController.MoveSelectorToCurrentCell();

                        if (selectorController.IsCellOccupied(selectorController.currentCellIndex))
                        {
                            selectorController.isSelecting = true;
                            selectorController.selectedObject = selectorController.boardCells[selectorController.currentCellIndex];

                            miniMenuCanvas.SetActive(true);
                        }

                        isBoardCell = true;
                        break;
                    }
                }

                if (!isBoardCell && clickedObject.CompareTag("Selectable"))
                {
                    selectorController.transform.position = clickedObject.position;
                    miniMenuCanvas.SetActive(true);
                }
            }
        }
    }
}