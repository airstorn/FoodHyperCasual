using System;
using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;

public delegate Transform OnIngridientAdded(IIngridient ingridient);
public interface IBurgerViewable
{
    event OnIngridientAdded IngridientAction;
    ref BurgerData GetData();
}
