using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Camera _raycastCamera;

    public OnInputPressed InputPressedAction;
    public delegate void OnInputPressed(bool b);

    public static Action<Vector3> OnTouchMoved;
    
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(_raycastCamera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                var selectable = hit.collider.GetComponent<ISelectable>();
                if (selectable != null)
                {
                    InputPressedAction = selectable.OnSelected;
                }
                else
                {
                    InputPressedAction = null;
                }
            }
            
            InputPressedAction?.Invoke(true);
            
        } 

        if (Input.GetMouseButtonUp(0))
        {
            InputPressedAction?.Invoke(false);
        }

        var mousePos = Input.mousePosition;
        mousePos.z = 2;
        OnTouchMoved?.Invoke(_raycastCamera.ScreenToWorldPoint(mousePos));
    }
}
