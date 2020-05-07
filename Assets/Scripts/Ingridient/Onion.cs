using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;

public class Onion : InputMovableBehaviour, IIngridient
{
    [SerializeField] private bool _placed = false;
    [SerializeField] private float _height = 0.05f;

    protected override void Update()
    {
        if(_placed == true)
            return;
        base.Update();
    }

    public void Place(Vector3 pos)
    {
        _placed = true;
        StartCoroutine(PlaceAnimation(pos));
    }

    private IEnumerator PlaceAnimation(Vector3 pos)
    {
        float elapsedTime = 0;
        float time = 1;
        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(transform.position, pos, elapsedTime / time);
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public float GetHeight()
    {
        return _height;
    }
}
