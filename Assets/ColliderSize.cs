using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class YourScriptName : MonoBehaviour
{
    // Your other variables and methods

    public void OnDrawGizmosSelected()
    {
        // Draw wireframe representation of the collider
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider2D>().bounds.size);
    }
}