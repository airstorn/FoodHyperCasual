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

        private Customer _currentCustomer;
        
        public static void Confirm()
        {
            GameLogic.Instance.ChangeState<RatingState>();
        }
        
        public void Deactivate(Action callback)
        {
            _ingridientsSpawner.Clear();
            
            callback?.Invoke();
        }

        private void Update()
        {
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
            _customerInteractor.DeclineCustomer(_currentCustomer);
            StartCoroutine(OrderAnimation());
        }

        private void AcceptCustomer()
        {
            StartCoroutine(CreateLevel());
            
        }
        
        private IEnumerator FirstStart()
        {
            yield return  StartCoroutine(_customerInteractor.PullCustomers(6));
            yield return new WaitForSeconds(1.5f);
            CreateOrder();
        }

        private IEnumerator OrderAnimation()
        {
            yield return new WaitForSeconds(1.5f);
            CreateOrder();
        }

    
        private void CreateOrder()
        {
            _currentCustomer = _customerInteractor.GetFirstCustomer();
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
