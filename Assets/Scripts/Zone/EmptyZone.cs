using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ingridient;
using UnityEngine;

public class EmptyZone : IInteractableZone
{
    private float _physicTime = 2;

    private MonoBehaviour _main;
    
    private Dictionary<IPhysicable, Coroutine> _waitingObjects = new Dictionary<IPhysicable, Coroutine>();
    public EmptyZone(GameObject main)
    {
        _main = main.GetComponent<MonoBehaviour>();
    }

    public bool InteractWith<T>(T interactionObject, InteractableZoneArgs args)
    {
        switch (args)
        {
            case InteractableZoneArgs.Add:
                return AddToWaitingObjects(interactionObject);
            case InteractableZoneArgs.Remove:
                return RemoveFromWaitingObjects(interactionObject);
            default:
                return false;
        }
    }

    private bool AddToWaitingObjects<T>(T interactionObject)
    {
        if (interactionObject is IPhysicable obj)
        {
            if (_waitingObjects.ContainsKey(obj) == false)
            {
                _waitingObjects.Add(obj, _main.StartCoroutine(ReturnAnimation(obj)) );
                
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    private bool RemoveFromWaitingObjects<T>(T removingObject)
    {
        var physical = removingObject as IPhysicable;
        if (_waitingObjects.ContainsKey(physical) == true)
        {
            var containedObject = _waitingObjects.First(element => element.Key == physical);

            _main.StopCoroutine(containedObject.Value);

            _waitingObjects.Remove(physical);
            
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator ReturnAnimation( IPhysicable physicable)
    {
        var body = physicable.GetBody();

        body.isKinematic = false;
        body.useGravity = true;

        yield return new WaitForSeconds(_physicTime);
      
            
        body.useGravity = false;
        body.isKinematic = true;

        _waitingObjects.Remove(physicable);
        
        Spawner.Instance.ReturnToSchedule(physicable as ISpawnable);
    }
}
