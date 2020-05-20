using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingZone : MonoBehaviour, IInteractableZone
{
    [SerializeField] private Transform _cookingObjectParent;
    [SerializeField] private Animator _anim;
    [SerializeField] private CookingZoneUI _ui;
    private Transform _cookable;
    private Coroutine _cookingProcess;
    
    public bool InteractWith<T>(T interactionObject, InteractableZoneArgs args)
    {
        switch (args)
        {
            case InteractableZoneArgs.Add:
                return TryCook(interactionObject);
            case InteractableZoneArgs.Remove:
                return RemoveFromCooking(interactionObject);
                default:
                    return false;
        }
        
    }

    private bool RemoveFromCooking<T>(T interactionObject)
    {
        if (_cookable != null)
        {
            _cookable.SetParent(null);
            _cookable = null;
            _anim.SetFloat("cook", 0);
            _ui.StopCooking();

            return  true;
        }
        else
        {
            return false;
        }
    }

    private bool TryCook<T>(T interactionObject)
    {
        if (_cookable != null)
            return false;
        
        if (interactionObject is ICookable cookable)
        {
            Cook(cookable);
            _anim.SetFloat("cook", 1);
            _ui.SetCookingState(cookable.GetTotalDuration(),cookable.GetReadiness());

            return true;
        }
        else
        {
            return false;
        }  
    }

    private void Cook(ICookable cookable)
    {
        _cookingProcess = StartCoroutine(CookAnimate(cookable));
        Debug.Log("cooking");
    }

    private IEnumerator CookAnimate(ICookable cookable)
    {
        float time = cookable.GetTotalDuration();
        float elapsed = cookable.GetReadiness();
        _cookable = cookable.GetTransform();
        
        _cookable.SetParent(_cookingObjectParent);
        _cookable.localPosition = Vector3.zero;
        
        while (elapsed < time && _cookable != null)
        {
            cookable.Cook(elapsed);
            _ui.SetCookingState(cookable.GetTotalDuration(),cookable.GetReadiness());
            
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
