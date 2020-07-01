using System;
using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;

namespace GameStates
{
    public class PlayState : MonoBehaviour, IGameState
    {
        [SerializeField] private GameObject _playerBurgerObject;
        [SerializeField] private Spawner _ingridientsSpawner;
        [SerializeField] private CustomerInteractor _customerInteractor;
        [SerializeField] private GameObject _firstBun;
        [SerializeField] private SelectableMover _mover;
        [SerializeField] private GameObject _nextButton;
        [SerializeField] private OrderPage _order;

        public Customer CurrentCustomer => _currentCustomer;
        
        private Customer _currentCustomer;
        private bool _levelSpawned;
     
        
        public void Deactivate(Action callback)
        {
            _ingridientsSpawner.Clear();
            _currentCustomer.SetAnimation(Customer.AnimationType.Order, false);
            callback?.Invoke();
        }

        public void Activate(Action activatAction)
        {
            _nextButton.SetActive(false);

            StartCoroutine(FirstStart());
            //
            
            // StartCoroutine(CreateLevel(null));
            activatAction?.Invoke();

            ClearPlayerBurger();
        }

        private void DeclineCustomer()
        {
            _customerInteractor.SendCustomerTo(_currentCustomer, CustomerInteractor.CustomerDirectionMove.Decline, CheckForLevelCompletion);
            _currentCustomer = null;
            StartCoroutine(FirstStart());
        }

        private void AcceptCustomer()
        {
            StartCoroutine(CreateLevel());
            
        }
        
        public static void Confirm()
        {
            GameLogic.Instance.ChangeState<RatingState>();
        }
        
        private IEnumerator FirstStart()
        {
            if (_levelSpawned == false)
            {
                yield return  StartCoroutine(_customerInteractor.PullCustomers(6));
                _levelSpawned = true;
            }

            if (_currentCustomer)
            {
                _customerInteractor.SendCustomerTo(_currentCustomer, CustomerInteractor.CustomerDirectionMove.Success, CheckForLevelCompletion);
            }
        
            ProceedCustomer();
        }

        private void CheckForLevelCompletion()
        {
            if(!_currentCustomer && _customerInteractor.CustomersCount == 0)
                Debug.Log("Level Parsed");
        }

        private void ProceedCustomer()
        {
            _currentCustomer = _customerInteractor.GetFirstCustomer();

            if (_currentCustomer)
            {
                _customerInteractor.SendCustomerTo(_currentCustomer, CustomerInteractor.CustomerDirectionMove.InteractionPoint, CreateOrder);
            }
        }

    
        private void CreateOrder()
        {
            Menu.Instance.SwitchPage<OrderPage>();
            _order.SetOrder(_currentCustomer.Request, DeclineCustomer, AcceptCustomer);
        }


        private IEnumerator InviteCustomer()
        {
            yield return _currentCustomer.Presenter.Present(_currentCustomer);
        }
        
        private IEnumerator CreateLevel()
        {
            _ingridientsSpawner.Clear();
            _mover.Unsubscribe();
            Menu.Instance.SwitchPage<GamePage>(); 

            PlaceBun(_firstBun);
            
            yield return InviteCustomer();
            
            yield return new WaitForSeconds(0.2f);
            
            yield return _ingridientsSpawner.SpawnElements(_currentCustomer.Burger.GetData());
            

            _mover.Subscribe();
        }

        private void OnValidate()
        {
            if (_playerBurgerObject.GetComponent<IBurgerViewable>() == null)
            {
                _playerBurgerObject = null;
            }
        }

        private void ClearPlayerBurger()
        {
            if(GameLogic.Instance.PlayerBurger.GetData()._ingridients == null)
                return;
            
            foreach (var ingridient in GameLogic.Instance.PlayerBurger.GetData()._ingridients)
            {
                var editable = ingridient as IEditable;
                if (editable != null) Destroy(editable.GetTransform().gameObject);
            }
            
            GameLogic.Instance.PlayerBurger.GetData()._ingridients.Clear();
        }
        
        private void PlaceBun(GameObject obj)
        {
            var inst = Instantiate(obj);
            inst.SetActive(true);
            GameLogic.Instance.PlayerBurger.GetData().AddIngridient(inst.GetComponent<IIngridient>());
        }
    }
}
