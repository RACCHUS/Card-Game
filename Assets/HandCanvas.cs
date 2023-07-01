using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasVisibility : MonoBehaviour
{
    public GameObject selectorObject;

    private Canvas canvas;
    private bool isCanvasActive = false;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    public void SetCanvasState(bool isVisible)
    {
        canvas.enabled = isVisible;
        isCanvasActive = isVisible;
        selectorObject.SetActive(!isVisible);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SetCanvasState(!isCanvasActive);
        }
    }
}