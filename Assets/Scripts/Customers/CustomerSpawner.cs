using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _customerTemplate;
    [SerializeField] private Transform _spawnOrigin;

    private Customer _currentCustomer;
    public void SpawnCustomer()
    {
        if (_currentCustomer == null)
        {
            var customer = Instantiate(_customerTemplate);
            customer.transform.position = _spawnOrigin.position;
            _currentCustomer = customer.GetComponent<Customer>();
        }
        
        _currentCustomer.SetVisible(true);
    }

    public void HideCustomer()
    {
        _currentCustomer.SetVisible(false);
    }
}
