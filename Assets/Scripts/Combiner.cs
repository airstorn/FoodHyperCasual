using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ingridient;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Combiner : MonoBehaviour
{
    [SerializeField] private List<IIngridient> _ingridients = new List<IIngridient>();

    private void OnTriggerEnter(Collider other)
    {
        var ingridient = other.GetComponent<IIngridient>();

        if (ingridient != null)
        {
            float height = _ingridients.Sum(ing => ing.GetHeight());
            _ingridients.Add(ingridient);
            ingridient.Place( new Vector3(transform.position.x, transform.position.y + height, transform.position.z));
        }
    }
}
