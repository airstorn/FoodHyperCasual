using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _customerTemplate;
    [SerializeField] private Transform _spawnOrigin;

    public Customer Customer
    {
        get
        {
            if (_currentCustomer == null)
            {
                return CreateCustomer();
            }
            else
            {
                return _currentCustomer;
            }
        }
    }

    private void Start()
    {
        SpawnCustomer();
    }

    private Customer _currentCustomer;
    
    public IEnumerator SpawnCustomer()
    {
        Customer.ClearRequest();
        
        Customer.SetVisible(true);
        Customer.SetRandomSkin();
        
        yield return new WaitForSeconds(1);
        
        Customer.CreateBurger();
        
        yield return new WaitForSeconds(0.5f);
        
        yield return StartCoroutine(Customer.AnimateRequest());
    }

    public void HideCustomer()
    {
        Customer.SetVisible(false);
    }

    private Customer CreateCustomer()
    {
        var customer = Instantiate(_customerTemplate);
        customer.transform.position = _spawnOrigin.position;
        _currentCustomer = customer.GetComponent<Customer>();
        return _currentCustomer;
    }
}
