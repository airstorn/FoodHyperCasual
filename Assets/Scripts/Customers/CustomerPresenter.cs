using System.Collections;
using UnityEngine;

namespace Customers
{
    public class CustomerPresenter : ICustomerPresenter
    {
        public IEnumerator Present(Customer customer)
        {
                customer.SetAnimation(Customer.AnimationType.Order, true);
                
                yield return new WaitForSeconds(0.5f);
        
                yield return customer.StartCoroutine(customer.AnimateRequest());
        }
    }
}