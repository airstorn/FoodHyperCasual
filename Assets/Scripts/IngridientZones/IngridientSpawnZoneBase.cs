using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;

public abstract class IngridientSpawnZoneBase : MonoBehaviour, IIngridientSpawnZone
{
    [SerializeField] private ISpawnable _ingridientTemplate;
    
    public virtual void Spawn(ISpawnable spawnObject)
    {
        _ingridientTemplate = spawnObject;
        _ingridientTemplate.Spawn(transform);
    }

    public bool IsEmpty()
    {
        return _ingridientTemplate == null;
    }

    public void Remove(ISpawnable spawnable)
    {
        _ingridientTemplate = null;
    }

    public ISpawnable GetHoldedSpawnable()
    {
        return _ingridientTemplate;
    }
}
