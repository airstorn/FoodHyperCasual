using System;
using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;

public class Sauce : InputMovableBehaviour, ISpawnable
{
    public override void OnSelected(bool selected)
    {
        _rotation = selected ? Quaternion.Euler(new Vector3(0,0, 165)) : Quaternion.Euler(Vector3.zero);

        base.OnSelected(selected);
    }
    public void Spawn(Transform origin)
    {
        _originPos = origin.position;
        _originRot = origin.rotation;

        transform.position = _originPos;
        transform.rotation = _originRot;
    }

    public void BackToPool()
    {
        gameObject.SetActive(false);
    }

    public bool Active()
    {
        return gameObject.activeInHierarchy;
    }
}
