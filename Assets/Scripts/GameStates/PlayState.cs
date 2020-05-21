using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStates
{
    public class PlayState : MonoBehaviour, IGameState
    {
        [SerializeField] private GameObject _ui;
        [SerializeField] private GameObject _playerBurgerObject;
        [SerializeField] private Spawner _ingridientsSpawner;
        [SerializeField] private CustomerSpawner _customerData;
        
        private IBurgerViewable _playerBurger;

        private void OnValidate()
        {
            if (_playerBurgerObject.GetComponent<IBurgerViewable>() == null)
            {
                _playerBurgerObject = null;
            }
        }

        public void Confirm()
        {
            StartCoroutine(ConfirmAnimation());
        }

        public IEnumerator ConfirmAnimation()
        {
            
            yield return null;
        }

        public void Activate(Action activatAction)
        {
            StartCoroutine(ActivateAnimation(activatAction));
        }

        private IEnumerator ActivateAnimation(Action callback)
        {
            Menu.Instance.SwitchPage(_ui, this); 

            callback?.Invoke();
            
            StartCoroutine(Createlevel());
            yield break;
        }

        private IEnumerator Createlevel()
        {
            //create customer
            _customerData.SpawnCustomer();
            
            yield return new WaitForSeconds(1);
            
            //create criteries
            
            //spawn ingridients
            
            yield return _ingridientsSpawner.SpawnElements(null);

            //...
        }

        public void Deactivate(Action callback = null)
        {
            
        }
    }
}
