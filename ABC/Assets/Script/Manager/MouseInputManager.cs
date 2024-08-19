using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputManager : MonoBehaviour
{
    public delegate void MouseClickedEventHandler(Vector3 position);
    public static event MouseClickedEventHandler OnMouseClicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click by mouse input");
            OnMouseClicked?.Invoke(Input.mousePosition);
            
        }
    }
}