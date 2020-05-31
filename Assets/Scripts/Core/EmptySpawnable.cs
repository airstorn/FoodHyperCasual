using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;

public class EmptySpawnable : ISpawnable
{
    public void Spawn(Transform origin)
    {
        throw new System.NotImplementedException();
    }

    public void Despawn()
    {
        throw new System.NotImplementedException();
    }
}
