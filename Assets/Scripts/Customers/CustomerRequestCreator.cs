using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ingridient;
using UnityEngine;
using Random = UnityEngine.Random;


public class CustomerRequestCreator : MonoBehaviour
{
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
    }

    public void CreateRequest(ref BurgerData data, Difficulty difficulty)
    {

        var burger = FillBurger(difficulty);

        foreach (var ing in burger)
        {
            var spawnedIng = SpawnIngridient(ing);
            data.AddIngridient(spawnedIng);
        }
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
}
