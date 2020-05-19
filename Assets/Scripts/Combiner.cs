using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ingridient;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Combiner : MonoBehaviour, IInteractableZone
{
    [SerializeField] private GameObject _burgerViewableObject;

    private IBurgerViewable _burger;
    private IIngridient _ingridientInside;
    
    private void Start()
    {
        _burger = _burgerViewableObject.GetComponent <IBurgerViewable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var ingridient = other.GetComponent<IIngridient>();

        if (ingridient != null)
        {
            _ingridientInside = ingridient;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _ingridientInside = null;
    }

    public bool InteractWith<T>(T interactionObject, InteractableZoneArgs args) 
    {
        var vacantObject = interactionObject as IIngridient;
        if(vacantObject == null)
            return false;
        if(_ingridientInside != vacantObject)
            return false;
        
        if(_burger.GetData()._ingridients.Contains(_ingridientInside))
            return false;
            
        Debug.Log("dsds");
        _burger.GetData().AddIngridient(_ingridientInside);
        _ingridientInside = null;
        return true;
    }
}
