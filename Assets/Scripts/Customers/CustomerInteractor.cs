using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CustomerSchedule), typeof(CustomerSpawner))]
public class CustomerInteractor : MonoBehaviour
{
    [SerializeField] private Transform _declinePoint;
    [SerializeField] private Transform _interactionPoint;
    private CustomerSchedule _schedule;
    private CustomerSpawner _spawner;

    private void Awake()
    {
        _schedule = GetComponent<CustomerSchedule>();
        _spawner = GetComponent<CustomerSpawner>();
    }

    public Customer GetFirstCustomer()
    {
        var customer = _schedule.GetFirstUnit();
        
        customer.MoveTo(_interactionPoint.position, customer.transform.rotation.eulerAngles, 1);
        
        return customer;
    }

    public void DeclineCustomer(Customer customer)
    {
        customer.MoveTo(_declinePoint.position, new Vector3(0, 90, 0), 1);
    }
    
    public IEnumerator PullCustomers(int count)
    {
        if(_schedule.Count != 0)
            yield break;
        
        for (int i = 0; i < count; i++)
        {
            var customer = _spawner.CreateCustomer();
            customer.SetRandomSkin();
            customer.SetAnimation(Customer.CustomerAnimationType.Order, false);
            _schedule.AddCustomer(customer);
            yield return null;
        }
    }
}
