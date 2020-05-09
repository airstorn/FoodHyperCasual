using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ingridient;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Combiner : MonoBehaviour
{
    [SerializeField] private GameObject _burgerViewableObject;

    private IBurgerViewable _burger;

    private void Start()
    {
        _burger = _burgerViewableObject.GetComponent <IBurgerViewable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var ingridient = other.GetComponent<IIngridient>();

        if (ingridient != null)
        {
           _burger.GetData().AddIngridient(ingridient);
        }
    }
}
