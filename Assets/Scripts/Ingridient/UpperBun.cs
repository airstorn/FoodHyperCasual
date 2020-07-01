using System;
using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;

public class UpperBun : SpawnableIngridient, IEditable
{
    [SerializeField] private AnimationCurve _spawnCurve;
    private IBurgerViewable _burger;


    private void SizeCallback(float obj)
    {
        transform.localScale = Vector3.one * _spawnCurve.Evaluate(obj);
    }

    public Transform GetTransform()
    {
        return transform;
    }
    // public void Spawn(Transform origin)
    // {
    //     var sizeData = new MovingUtility.FloatLerpContainer()
    //     {
    //         Duration = 0.4f,
    //         StartValue = 0,
    //         TargetValue = 1
    //     };
    //     MovingUtility.LerpFloat(sizeData, SizeCallback);
    // }
    //
    // public void Despawn()
    // {
    // }

}
