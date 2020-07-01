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
        customer.transform.DOMove(_interactionPoint.position, 1);
        return customer;
    }

    public void UpdateSchedule()
    {
        _schedule.UpdateSchedule();
        
    }

    public void DeclineCustomer(Customer customer)
    {
        customer.transform.DORotate(new Vector3(0, 90, 0), 1f);
        customer.transform.DOMove(_declinePoint.position, 2f);
        customer.SetAnimation(Customer.CustomerAnimationType.MoveVertical, true);
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
