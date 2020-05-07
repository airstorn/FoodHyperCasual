using System;
using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;

public class Sauce : InputMovableBehaviour
{
    public override void OnSelected(bool selected)
    {
        _rotation = selected ? Quaternion.Euler(new Vector3(0,0, 165)) : Quaternion.Euler(Vector3.zero);

        base.OnSelected(selected);
    }
}
