using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;

public class SauceIngridient : MonoBehaviour, IIngridient, IEditable
{
    [SerializeField] private float _height = 0.01f;
    public float GetHeight()
    {
        return _height;
    }

    public ISpawnable GetSpawnable()
    {
        return null;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
