using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;

public interface IIngridientZone
{
    void Spawn(ISpawnable spawnObject);
    bool IsSpawned();
}
