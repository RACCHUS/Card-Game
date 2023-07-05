using UnityEngine;

public class HandController : MonoBehaviour
{
    public GameObject handObject;
    public GameObject selectorObject;

    private bool isHandEnabled = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleHandAndSelector();
        }
    }

    public void ToggleHandAndSelector()
    {
        isHandEnabled = !isHandEnabled;
        handObject.SetActive(isHandEnabled);
        selectorObject.SetActive(!isHandEnabled);
    }
}