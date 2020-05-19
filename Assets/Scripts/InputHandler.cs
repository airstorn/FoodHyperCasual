using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Camera _raycastCamera;
    [SerializeField] private Transform _zOffset;

    public static Action<bool> OnClick;

    public static Action<Vector3> OnTouchMoved;
    public static Action<Vector3> OnMoveDelta;

    private ISelectable _currentSelectedSelectable;

    private Vector3 _oldMousePos;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClick?.Invoke(true);
        } 

        if (Input.GetMouseButtonUp(0))
        {
            OnClick?.Invoke(false);
        }

        _oldMousePos = Input.mousePosition;
        _oldMousePos.z = 2;

        _oldMousePos = _raycastCamera.ScreenToWorldPoint(_oldMousePos);
        OnTouchMoved?.Invoke(_oldMousePos);
    }
}
