using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;

public class SauceIngridient : MonoBehaviour, IIngridient
{
    [SerializeField] private float _height = 0.01f;
    public void Place(Vector3 pos)
    {
        transform.position = pos;
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }

    public float GetHeight()
    {
        return _height;
    }

    public ISpawnable GetSpawnable()
    {
        return null;
    }
}
