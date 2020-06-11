using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

public class MoneyHolder : MonoBehaviour
{
    public int CurrentMoney => _currentMoney;
    
    private IMoneyListener[] _handlers;
    private Action<int> OnMoneyChanged;
    private int _currentMoney;

    private readonly string _moneyKey = "money";
    
    private void Awake()
    {
        _handlers  = Resources.FindObjectsOfTypeAll<MonoBehaviour>().OfType<IMoneyListener>().ToArray();
        AddMoney(LoadMoney());
        
        foreach (var listener in _handlers)
        {
            Subscribe(listener);
        }
        
    }

    public void Subscribe<T>(T handler) where T : IMoneyListener
    {
        OnMoneyChanged += handler.UpdateValue;
        handler.UpdateValue(CurrentMoney);
    }

    public void AddMoney(int money)
    {
        _currentMoney += money;
        OnMoneyChanged?.Invoke(CurrentMoney);
        SaveMoney();
    }

    private int LoadMoney() 
    {
        return PlayerPrefs.GetInt(_moneyKey, 0);
    }

    private void SaveMoney()
    {
        PlayerPrefs.SetInt(_moneyKey, CurrentMoney);
    }
}
