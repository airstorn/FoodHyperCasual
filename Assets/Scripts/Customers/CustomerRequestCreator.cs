using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using Ingridient;
using UnityEngine;
using Random = UnityEngine.Random;


public class CustomerRequestCreator : MonoBehaviour, ILevelListener
{
    [SerializeField] private Difficulty[] _difficultyPattern = new Difficulty[10];
    private int _offset;
    
    private IBurgerLoader _categoryGetter;
    
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
    private void Awake()
    {
        _categoryGetter = GetComponent<IBurgerLoader>();
        FindObjectOfType<LevelManager>().Subscribe(this);
    }

    public Customer.CustomerRequest CreateRequest(ref BurgerData data)
    {
        var burger = FillBurger(_difficultyPattern[_offset]);

        foreach (var ing in burger)
        {
            var spawnedIng = SpawnIngridient(ing);
            data.AddIngridient(spawnedIng);
        }
        
        Customer.CustomerRequest request = new Customer.CustomerRequest()
        {
            Burger = data,
            Price = Random.Range(0, 50)
        };

        return request;
    }

    public void ClearRequest(ref BurgerData data)
    {
        foreach (var ingridient in data._ingridients)
        {
            var ing = ingridient as IEditable;
            Destroy(ing.GetTransform().gameObject);
        }
        
        data._ingridients.Clear();
    }

    private IIngridient[] FillBurger(Difficulty dif)
    {
        var data=   _categoryGetter.GetBurgerList(dif);

        return data[Random.Range(0, data.Count)].BurgerIngridients.ToArray();
    }

    private IIngridient SpawnIngridient<T>(T ing) where T : IIngridient
    {
        var obj = ing as IEditable;
        var spawnedObj = Instantiate(obj.GetTransform().gameObject);
        return spawnedObj.GetComponent<IIngridient>();
    }

    public void SetLevel(int level)
    {
        _offset = level % 10;
    }
}
