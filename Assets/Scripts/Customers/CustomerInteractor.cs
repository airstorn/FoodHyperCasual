using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Customers
{
    [RequireComponent(typeof(CustomerSchedule), typeof(CustomerSpawner))]
    public class CustomerInteractor : MonoBehaviour
    {
        [SerializeField] private Transform _declinePoint;
        [SerializeField] private Transform _successPoint;
        [SerializeField] private Transform _interactionPoint;

        public int CustomersCount => _schedule.Count;
    
        private CustomerSchedule _schedule;
        private CustomerSpawner _spawner;

        public enum CustomerDirectionMove
        {
            Decline,
            InteractionPoint,
            Success
        }

        private void Awake()
        {
            _schedule = GetComponent<CustomerSchedule>();
            _spawner = GetComponent<CustomerSpawner>();
        }

        public Customer GetFirstCustomer()
        {
            var customer = _schedule.GetFirstUnit();
        
        
            return customer;
        }

        public void SendCustomerTo(Customer customer, CustomerDirectionMove direction, TweenCallback callback = null)
        {
            switch (direction)
            {
                case CustomerDirectionMove.Decline:
                    customer.MoveTo(_declinePoint.position, new Vector3(0, 90, 0), 1, () =>
                    {
                        Destroy(customer.gameObject);
                        callback?.Invoke();
                    });
                    break;
                case CustomerDirectionMove.Success:
                    customer.MoveTo(_successPoint.position, new Vector3(0, -90, 0), 1, () =>   
                    {
                        Destroy(customer.gameObject);
                        callback?.Invoke();
                    });
                    break; 
                case CustomerDirectionMove.InteractionPoint:
                    customer.MoveTo(_interactionPoint.position, new Vector3(0, 0, 0), 1, callback);
                    break;
            }
        }

        public IEnumerator PullCustomers(int count)
        {
            if(_schedule.Count != 0)
                yield break;
        
            for (int i = 0; i < count; i++)
            {
                var customer = _spawner.CreateCustomer();
                customer.RandomizeSkin();
            
                customer.SetScheduleNumber(i);
                customer.CreateRequest();
            
                customer.SetAnimation(Customer.AnimationType.Order, false);
                _schedule.AddCustomer(customer);
                yield return null;
            }
            
            LevelStatus.Instance.SetTotalCustomers(count);
        }
    }
}
