using System;
using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;

namespace GameStates
{
    public class PlayState : MonoBehaviour, IGameState
    {
        [SerializeField] private GameLogic _logic;
        [SerializeField] private GameObject _ui;
        [SerializeField] private GameObject _playerBurgerObject;
        [SerializeField] private Spawner _ingridientsSpawner;
        [SerializeField] private CustomerSpawner _customerData;
        [SerializeField] private GameObject _firstBun;
        [SerializeField] private GameObject _secondBun;
        

  

        public void Confirm()
        {
            PlaceBun(_secondBun);
            
            _logic.ChangeState<RatingState>();
        }
        
        public void Deactivate(Action callback)
        {
            StartCoroutine(HideCustomer(callback));
            _ingridientsSpawner.Clear();

        }

        public void Activate(Action activatAction)
        {
            StartCoroutine(Createlevel(activatAction));
        }

        private IEnumerator Createlevel(Action callback)
        {
            _ingridientsSpawner.Clear();
            Menu.Instance.SwitchPage(_ui, this); 
            
            ClearPlayerBurger();
            
            PlaceBun(_firstBun);
            
            yield return StartCoroutine(_customerData.SpawnCustomer());
            
            yield return new WaitForSeconds(0.2f);
            
            yield return _ingridientsSpawner.SpawnElements(null);
            
            callback?.Invoke();
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
            if(_logic.PlayerBurger.GetData()._ingridients == null)
                return;
            
            foreach (var ingridient in _logic.PlayerBurger.GetData()._ingridients)
            {
                var editable = ingridient as IEditable;
                if (editable != null) Destroy(editable.GetTransform().gameObject);
            }
            
            _logic.PlayerBurger.GetData()._ingridients.Clear();
        }
        
        private void PlaceBun(GameObject obj)
        {
            var inst = Instantiate(obj);
            inst.SetActive(true);
            _logic.PlayerBurger.GetData().AddIngridient(inst.GetComponent<IIngridient>());
        }

       
        private IEnumerator HideCustomer(Action ac)
        {
            _customerData.Customer.SetVisible(false);
            yield return new WaitForSeconds(1);
            ac.Invoke();
        }
    }
}
