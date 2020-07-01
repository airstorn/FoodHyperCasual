using System.Collections;

namespace Customers
{
    public interface ICustomerPresenter
    {
        IEnumerator Present(Customer customer);
        
    }
}