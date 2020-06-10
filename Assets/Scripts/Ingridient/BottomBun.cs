using System;
using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;

public class BottomBun : MonoBehaviour, IIngridient, IEditable, ISpawnable
{
    [SerializeField] private AnimationCurve _spawnCurve;
    [SerializeField] private float _height = 0.016f;
    private IBurgerViewable _burger;

    private IEnumerator SpawnAnimate()
    {
        float time = 1;
        float elapsed = 0;
        while (elapsed <= time)
        {
            transform.localScale = Vector3.one * _spawnCurve.Evaluate( elapsed/ time);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    public float GetHeight()
    {
        return _height;
    }

    public GameObject GetObject()
    {
        return gameObject;
    }

    public Transform GetTransform()
    {
        return transform;
    }


    public void Spawn(Transform origin)
    {
    }

    public void Despawn()
    {
        throw new NotImplementedException();
    }
}
