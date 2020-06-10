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
        [SerializeField] private CustomerSpawner _customerData;
        [SerializeField] private GameObject _firstBun;
        [SerializeField] private GameObject _secondBun;
        [SerializeField] private SelectableMover _mover;
        [SerializeField] private PressPlay _playAction;
        [SerializeField] private GameObject _nextButton;

        
        public void Confirm()
        {
            PlaceBun(_secondBun);
            
            GameLogic.Instance.ChangeState<RatingState>();
        }
        
        public void Deactivate(Action callback)
        {
            _ingridientsSpawner.Clear();
            GameLogic.Instance.PlayerBurger.GetData().OnIngridientAdded -= NextButtonUnlock;
            _customerData.Customer.SetVisible(false);
            callback?.Invoke();
        }

        public void Activate(Action activatAction)
        {
            _playAction.Subscribe();
            Menu.Instance.SwitchPage<MenuPage>(); 
            _nextButton.SetActive(false);
            
            ClearPlayerBurger();
        }

        private void NextButtonUnlock(IIngridient ingridient)
        {
            _nextButton.SetActive(true);
        }

        private void OnPlay()
        {
            StartCoroutine(Createlevel(null));
        }

        private IEnumerator Createlevel(Action callback)
        {
            _ingridientsSpawner.Clear();
            _mover.Unsubscribe();
            Menu.Instance.SwitchPage<GamePage>(); 

            
            PlaceBun(_firstBun);
            
            GameLogic.Instance.PlayerBurger.GetData().OnIngridientAdded += NextButtonUnlock;
            
            yield return StartCoroutine(_customerData.SpawnCustomer());
            
            yield return new WaitForSeconds(0.2f);
            
            yield return _ingridientsSpawner.SpawnElements();
            

            _mover.Subscribe();
            callback?.Invoke();
        }

        private void Start()
        {
            PressPlay.OnPlay += OnPlay;
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
