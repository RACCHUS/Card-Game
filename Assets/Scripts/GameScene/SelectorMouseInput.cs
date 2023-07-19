using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Detect the mouse click
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null)
            {
                // Move the selector to the clicked square
                Vector3 targetPosition = hit.collider.transform.position;
                targetPosition.z = transform.position.z; // Preserve the Z coordinate
                transform.position = targetPosition;
            }
        }
    }
}
