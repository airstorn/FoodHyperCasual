using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;

public interface IIngridientSpawnZone
{
    void Spawn(ISpawnable spawnObject);
    bool IsEmpty();
    void Remove(ISpawnable spawnable);
    ISpawnable GetHoldedSpawnable();
}
