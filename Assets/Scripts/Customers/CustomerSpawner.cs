using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _customerTemplate;
    [SerializeField] private Transform _spawnOrigin;
    public Customer Customer => _currentCustomer;
    
    private Customer _currentCustomer;
    
    public IEnumerator SpawnCustomer()
    {
        if (!_currentCustomer)
        {
            var customer = Instantiate(_customerTemplate);
            customer.transform.position = _spawnOrigin.position;
            _currentCustomer = customer.GetComponent<Customer>();
        }
        
        _currentCustomer.ClearRequest();
        
        _currentCustomer.SetVisible(true);
        _currentCustomer.SetRandomSkin();
        
        yield return new WaitForSeconds(1);
        
        _currentCustomer.CreateBurger();
        
        yield return new WaitForSeconds(0.5f);
        
        yield return StartCoroutine(_currentCustomer.AnimateRequest());
    }

    public void HideCustomer()
    {
        _currentCustomer.SetVisible(false);
    }
}
