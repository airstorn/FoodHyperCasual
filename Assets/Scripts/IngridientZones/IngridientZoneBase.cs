using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;

public abstract class IngridientZoneBase : MonoBehaviour, IIngridientZone
{
    [SerializeField] private ISpawnable _ingridientTemplate;
    
    public virtual void Spawn(ISpawnable spawnObject)
    {
        _ingridientTemplate = spawnObject;
        _ingridientTemplate.Spawn(transform);
    }

    public bool IsSpawned()
    {
        return _ingridientTemplate != null;
    }
}
