using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;

public class SauceIngridient : MonoBehaviour, IIngridient, IEditable, IRatable
{
    [SerializeField] private float _height = 0.01f;
    [SerializeField] private GameObject _spawnable;
    public float GetHeight()
    {
        return _height;
    }

    public GameObject GetObject()
    {
        return _spawnable;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public float GetRating()
    {
        return 1;
    }
}
