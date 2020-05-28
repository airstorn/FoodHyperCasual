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
        [SerializeField] private ParticleSystem _confirmParticles;
        [SerializeField] private GameObject _firstBun;
        [SerializeField] private GameObject _secondBun;
        
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
            PlaceBun(_secondBun);
            
            _confirmParticles.Play();
            _logic.ChangeState<RatingState>();
        }
        
        private void PlaceBun(GameObject obj)
        {
            var inst = Instantiate(obj);
            inst.SetActive(true);
            _logic.PlayerBurger.GetData().AddIngridient(inst.GetComponent<IIngridient>());
        }
       

        public void Activate(Action activatAction)
        {
            StartCoroutine(ActivateAnimation(activatAction));
        }

        private IEnumerator ActivateAnimation(Action callback)
        {
            Menu.Instance.SwitchPage(_ui, this); 
            PlaceBun(_firstBun);
            callback?.Invoke();
            
            StartCoroutine(Createlevel());
            yield break;
        }

        private IEnumerator Createlevel()
        {
            //create customer
            yield return StartCoroutine(_customerData.SpawnCustomer());
            
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
