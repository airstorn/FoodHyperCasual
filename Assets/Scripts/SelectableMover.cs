using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableMover : MonoBehaviour
{
    [SerializeField] private Camera _raycastCamera;
    [SerializeField] private LayerMask _selectableMask;
    [SerializeField] private LayerMask _placeMask;

    private ISelectable _selectable;
    private ISelectable _currentSelectable
    {
        get { return _selectable; }
        set
        {
            _selectable?.OnSelectedAction?.Invoke(false);
            _selectable = value;
            _selectable?.OnSelectedAction?.Invoke(true);
        }
    }
    private IInteractableZone _defaultZone;
    private Coroutine _movingRoutine;
    private Vector3 _movingPos;

    private void Awake()
    {
        _defaultZone = new EmptyZone(gameObject);
    }

    
    public void Subscribe()
    {
        InputHandler.OnClick += ClickHandler;
        InputHandler.OnTouchMoved += SetMovingOffset;
    }
    public void Unsubscribe()
    {
        InputHandler.OnClick -= ClickHandler;
        InputHandler.OnTouchMoved -= SetMovingOffset;
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
               _currentSelectable.SetInteractableZone(null);
               // _defaultZone.InteractWith(_currentSelectable, InteractableZoneArgs.Remove);
           }
        }
        else
        {
            if (_currentSelectable != null)
            {
                var zone = TryCatchObject<IInteractableZone>(_placeMask);

                if (zone != null)
                {
                   var added = _currentSelectable.SetInteractableZone(zone);
                   if (added == false)
                   {
                       _currentSelectable.SetInteractableZone(_defaultZone);
                   }
                }
                else
                {
                    _currentSelectable.SetInteractableZone(_defaultZone);
                }

                _currentSelectable = null;
            }
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
