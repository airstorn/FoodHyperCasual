using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IngridientZoneBase : MonoBehaviour, IIngridientZone
{
    [SerializeField] private GameObject _ingridientTemplate;
    
    public virtual void Interact()
    {
        
    }
}
