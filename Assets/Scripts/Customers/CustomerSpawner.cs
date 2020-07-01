using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _customerTemplate;
    [SerializeField] private Transform _spawnOrigin;
    private CustomerSchedule _schedule;


    private void Start()
    {
        _schedule = GetComponent<CustomerSchedule>();
    }

    public Customer CreateCustomer()
    {
        var customer = Instantiate(_customerTemplate);

        customer.name = "Customer " + Random.Range(0, 1000);
        customer.transform.position = _spawnOrigin.position;
        return customer.GetComponent<Customer>();
    }
}
