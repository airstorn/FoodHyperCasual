using System;
using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;

public class Bun : MonoBehaviour, IIngridient
{
    [SerializeField] private GameObject _burgerObject;
    [SerializeField] private AnimationCurve _spawnCurve;
    [SerializeField] private float _height = 0.016f;
    private IBurgerViewable _burger;

    private void OnValidate()
    {
        if (_burgerObject.GetComponent<IBurgerViewable>() == null)
            _burgerObject = null;
    }

    private void Start()
    {
        _burger = _burgerObject.GetComponent<IBurgerViewable>();
        _burger.GetData().AddIngridient(this);
    }

    public void Place(Vector3 pos)
    {
        transform.position = pos;
        StartCoroutine(SpawnAnimate());
    }

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

    public ISpawnable GetSpawnable()
    {
        return null;
    }
}
