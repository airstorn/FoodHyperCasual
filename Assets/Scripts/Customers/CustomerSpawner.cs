using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _customerTemplate;
    [SerializeField] private Transform _spawnOrigin;

    private Customer _currentCustomer;
    
    public IEnumerator SpawnCustomer()
    {
        if (_currentCustomer == null)
        {
            var customer = Instantiate(_customerTemplate);
            customer.transform.position = _spawnOrigin.position;
            _currentCustomer = customer.GetComponent<Customer>();
        }
        
        _currentCustomer.SetVisible(true);
        yield return new WaitForSeconds(1);
        _currentCustomer.CreateBurger();
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(_currentCustomer.BurgerRotation(-45));
        yield return StartCoroutine(_currentCustomer.AnimateRequest());
    }

    public void HideCustomer()
    {
        _currentCustomer.SetVisible(false);
    }
}
