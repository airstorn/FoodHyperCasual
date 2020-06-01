using System;
using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;

public class UpperBun : MonoBehaviour, IIngridient, IEditable, ISpawnable
{
    [SerializeField] private AnimationCurve _spawnCurve;
    [SerializeField] private float _height = 0.016f;
    private IBurgerViewable _burger;


    private void SizeCallback(float obj)
    {
        transform.localScale = Vector3.one * _spawnCurve.Evaluate(obj);
    }


    public float GetHeight()
    {
        return _height;
    }
    
    public Transform GetTransform()
    {
        return transform;
    }

    public void Spawn(Transform origin)
    {
        var sizeData = new MovingUtility.FloatLerpContainer()
        {
            Duration = 0.4f,
            StartValue = 0,
            TargetValue = 1
        };
        MovingUtility.LerpFloat(sizeData, SizeCallback);
    }

    public void Despawn()
    {
    }
}
