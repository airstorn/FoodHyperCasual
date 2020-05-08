using System.Collections.Generic;
using Ingridient;
using UnityEngine;

public class Burger
{
    [SerializeField] private List<IIngridient> _containedIngridients = new List<IIngridient>();

    public List<IIngridient> Ingridients => _containedIngridients;

    public Burger(List<IIngridient> ingridients)
    {
        _containedIngridients = ingridients;
    }
}