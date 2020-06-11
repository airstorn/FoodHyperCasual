using System;
using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;

public class Tomato : SpawnableIngridient, IEditable
{
    public Transform GetTransform()
    {
        return transform;
    }
}
