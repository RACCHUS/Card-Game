using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public GameObject hand;
    public GameObject selector;
    public GameObject selectedObjectPrefab; // Prefab for the new object

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("HandSelectable"))
            {
                DisableHand();
                EnableSelector();

                // Create a new object with the selected sprite
                CreateSelectedObject(hit.transform.GetComponent<SpriteRenderer>().sprite);
            }
        }
    }

    private void DisableHand()
    {
        hand.SetActive(false);
    }

    private void EnableSelector()
    {
        selector.SetActive(true);
    }

    private void CreateSelectedObject(Sprite sprite)
    {
        GameObject newObject = Instantiate(selectedObjectPrefab, Vector3.zero, Quaternion.identity);
        newObject.GetComponent<SpriteRenderer>().sprite = sprite;
        // Additional customization or positioning of the new object can be done here
    }
}