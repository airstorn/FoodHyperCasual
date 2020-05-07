using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Camera _raycastCamera;

    public OnInputPressed InputPressedAction;
    public delegate void OnInputPressed();
    
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InputPressedAction?.Invoke();
            RaycastHit hit;
            if (Physics.Raycast(_raycastCamera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Debug.Log(hit.collider.name);
            }
        }
    }
}
