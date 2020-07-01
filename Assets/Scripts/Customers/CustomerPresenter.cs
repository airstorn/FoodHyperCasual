using System.Collections;
using UnityEngine;

namespace Customers
{
    public class CustomerPresenter : ICustomerPresenter
    {
        public IEnumerator Present(Customer customer)
        {
                customer.ClearRequest();
        
                customer.SetAnimation(Customer.CustomerAnimationType.Order, true);
                
                yield return new WaitForSeconds(0.5f);
                
                customer.CreateBurger();
        
                yield return new WaitForSeconds(0.5f);
        
                yield return customer.StartCoroutine(customer.AnimateRequest());
        }
    }
}