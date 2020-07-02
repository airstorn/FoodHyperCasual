using System;
using UnityEngine;

public class LevelStatus : MonoBehaviour
{
    public int SuccessfulCustomers => _successfulCustomers;
    public int TotalCustomers => _totalCustomersCount;
    
    [SerializeField] private int _totalCustomersCount;
    [SerializeField] private int _successfulCustomers;

    public void SetTotalCustomers(int count) =>
        _totalCustomersCount = count;

    public void SetSuccessfulCustomers(int newValue) =>
        _successfulCustomers = Mathf.Clamp( newValue, 0, _totalCustomersCount);

    public void ResetLevelData()
    {
        _successfulCustomers = 0;
        _totalCustomersCount = 0;
    }

    public static LevelStatus Instance;
    private void Awake()
    {
        Instance = this;
    }
}