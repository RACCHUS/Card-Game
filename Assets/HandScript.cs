using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasVisibility : MonoBehaviour
{
    private Canvas canvas;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            canvas.enabled = !canvas.enabled;
        }
    }
}