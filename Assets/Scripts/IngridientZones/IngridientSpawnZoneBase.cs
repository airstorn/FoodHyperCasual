using System;
using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;

public abstract class IngridientSpawnZoneBase : MonoBehaviour, IIngridientSpawnZone
{
    [SerializeField] private string name;

    protected ISpawnable ingridientTemplate;
    
    [SerializeField] protected ISpawnable _ingridientTemplate
    {
        get { return ingridientTemplate; }
        set
        {
            name = value.ToString();
            ingridientTemplate = value;
        }
    }

    private ISpawnable _empty;

    private void Start()
    {
        _empty = new EmptySpawnable();
    }

    public virtual void Spawn(ISpawnable spawnObject)
    {
        _ingridientTemplate = spawnObject;
        _ingridientTemplate.Spawn(transform);
    }

    public bool IsEmpty()
    {
        return _ingridientTemplate == _empty;
    }

    public void Remove(ISpawnable spawnable)
    {
        _ingridientTemplate = _empty;
    }

    public ISpawnable GetHoldedSpawnable()
    {
        return _ingridientTemplate;
    }
}
