using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableMover : MonoBehaviour
{
    [SerializeField] private Camera _raycastCamera;
    [SerializeField] private LayerMask _selectableMask;
    [SerializeField] private LayerMask _placeMask;
    
    private ISelectable _currentSelectable;
    private IInteractableZone _defaultZone;
    private Coroutine _movingRoutine;
    private Vector3 _movingPos;
    
    private void Start()
    {
        InputHandler.OnClick += ClickHandler;
        InputHandler.OnTouchMoved += SetMovingOffset;
        
        _defaultZone = new EmptyZone(gameObject);
    }

    private void SetMovingOffset(Vector3 obj)
    {
        _movingPos = obj;
    }

    private void ClickHandler(bool obj)
    {
        if (obj == true)
        {
           _currentSelectable = TryCatchObject<ISelectable>(_selectableMask);

           if (_currentSelectable != null)
           {
               _movingRoutine = StartCoroutine(MoveSelectable(_currentSelectable.Move));
               _defaultZone.InteractWith(_currentSelectable, InteractableZoneArgs.Remove);
           }
        }
        else
        {
            var zone = TryCatchObject<IInteractableZone>(_placeMask);

            bool success = false;

            if (zone != null)
                success = zone.InteractWith(_currentSelectable, InteractableZoneArgs.Add);
            
            if(success == false)
                _defaultZone.InteractWith(_currentSelectable, InteractableZoneArgs.Add);
            
            _currentSelectable = null;
        }
    }

    private IEnumerator MoveSelectable(Action<Vector3> selectableMoveAction)
    {
        while (_currentSelectable != null)
        {
            selectableMoveAction?.Invoke(_movingPos);

            yield return null;
        }
    }

    private T TryCatchObject<T>(LayerMask mask)
    {
        RaycastHit hit;
        object vacantObject = null;
        if (Physics.Raycast(_raycastCamera.ScreenPointToRay(Input.mousePosition), out hit, 1000, mask))
        {
            vacantObject = hit.collider.GetComponent<T>();
        }

        return (T) vacantObject;
    }
}
