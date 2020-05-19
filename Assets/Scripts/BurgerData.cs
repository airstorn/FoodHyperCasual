using System;
using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;

public struct BurgerData
{
    public List<IIngridient> _ingridients;
    public Action<IIngridient> OnIngridientAdded;

    public void AddIngridient(IIngridient ingridient)
    {
        if(_ingridients == null)
            _ingridients = new List<IIngridient>(); 
        
        _ingridients.Add(ingridient);
        
        OnIngridientAdded?.Invoke(ingridient);
    }
}
