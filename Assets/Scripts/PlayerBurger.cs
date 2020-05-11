using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ingridient;
using UnityEngine;

public class PlayerBurger : MonoBehaviour, IBurgerViewable
{
    public BurgerData ContainedData;
    [SerializeField] private Transform _origin;
    [SerializeField] private GameObject _bunObject;
    [SerializeField] private GameObject _secondBunObject;

    private void Start()
    {
        ContainedData.OnIngridientAdded += OnIngridientAdded;
        
        ContainedData._ingridients = new List<IIngridient>();
        PlaceBun(_bunObject);
    }
    public void Show(BurgerData data)
    {
        
    }

    public void Confirm()
    {
        PlaceBun(_secondBunObject);
    }

    private void PlaceBun(GameObject obj)
    {
        obj.SetActive(true);
        ContainedData.AddIngridient(obj.GetComponent<IIngridient>());
    }

    private void OnDisable()
    {
        ContainedData.OnIngridientAdded -= OnIngridientAdded;
    }

    private void OnIngridientAdded(IIngridient obj)
    {
        Debug.Log(obj + " added");
        obj.Place(new Vector3(transform.position.x, transform.position.y + ContainedData._ingridients.Sum(el => el.GetHeight()), transform.position.z));
    }


    public BurgerData GetData()
    {
        return ContainedData;
    }
}
