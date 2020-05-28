using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ingridient;
using UnityEngine;

public class CustomerBurger : MonoBehaviour, IBurgerViewable
{
    [SerializeField] private BurgerData _data;
    [SerializeField] private Transform _requestPosition;
    
    public event OnIngridientAdded IngridientAction;

    private void Awake()
    {
        _data._ingridients = new List<IIngridient>();
        _data.OnIngridientAdded += OnIngridientAdded;
    }

    private void OnIngridientAdded(IIngridient obj)
    {
        var position = Vector3.zero;
        var vacantPos = new Vector3(position.x,
            position.y + _data._ingridients.Sum(el => el != obj ? el.GetHeight() * 2 : 0),
            position.z);
        
      
            var t = obj as IEditable;
            
            t.GetTransform().SetParent(_requestPosition);

            t.GetTransform().localPosition = vacantPos;
            t.GetTransform().gameObject.layer = 0;
            
            t.GetTransform().localScale = Vector3.one * 1.5f;
    }

    public ref BurgerData GetData()
    {
        return ref _data;
    }
}
