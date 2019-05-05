using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseTest : MonoBehaviour {

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        Debug.Log("Enter");
    }

    private void OnMouseExit()
    {
        Debug.Log("Leave");
    }
}
