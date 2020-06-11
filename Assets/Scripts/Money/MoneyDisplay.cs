using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour, IMoneyListener
{
    [SerializeField] private Text _moneyText;
    
    public void UpdateValue(int value)
    {
        _moneyText.text = value.ToString();
    }
}
