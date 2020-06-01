using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ingridient;
using UnityEngine;
using Random = UnityEngine.Random;


public class CustomerRequestCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] _ingridientsCash;
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
    private void Awake()
    {
        _ingridientsCash = Resources.LoadAll<GameObject>("ingridients/");
    }

    public void CreateRequest(ref BurgerData data, Difficulty difficulty)
    {

        var burger = FillBurger(difficulty);

        foreach (var ing in burger)
        {
            data.AddIngridient(ing);
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

        List<IIngridient> ings = new List<IIngridient>();
        // switch (dif)
        // {
        //     case Difficulty.Easy:
        //         burger = new IIngridient[Random.Range(4, 6)];
        //         break;
        //     case Difficulty.Medium:
        //         burger = new IIngridient[Random.Range(5, 8)];
        //         break;
        //     case Difficulty.Hard:
        //         burger = new IIngridient[Random.Range(6, 9)];
        //         break;
        //     default:
        //         burger = new IIngridient[0];
        //         break;
        // }

        ings.Add(CreateIngridientByType<BottomBun>());
        ings.Add(CreateIngridientByType<Steak>());
        ings.Add(CreateIngridientByType<SauceIngridient>());
        ings.Add(CreateIngridientByType<UpperBun>());
        
        

        return ings.ToArray();
    }

    private IIngridient CreateIngridientByType<T>() where T : IIngridient
    {
        var findedObj = _ingridientsCash.First(s => s.GetComponent<T>() != null);
        var obj = Instantiate(findedObj).GetComponent<IIngridient>();
        return obj;
    }
}
