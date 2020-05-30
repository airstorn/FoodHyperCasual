using System;
using System.Collections;
using System.Collections.Generic;
using GameStates;
using UnityEngine;

public class RatingState : MonoBehaviour, IGameState
{
    [SerializeField] private GameLogic _logic;
    [SerializeField] private CustomerSpawner _customerSpawner;
    [SerializeField] private GameObject _ui;
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private MovingRails _cameraMovement;
    [SerializeField] private MovingRails _burgerMovement;

    [Serializable]
    public struct MovingRails
    {
        public Transform Target;
        public Transform From;
        public Transform To;

    }
    public void Activate(Action activatAction)
    {
        StartCoroutine(AnimateActivate(activatAction));
       
    }

    private IEnumerator AnimateActivate(Action activatAction)
    { 
        var cameraData = new MovingUtility.MovingContainer()
        {
            originPos = _cameraMovement.From.position,
            targetPos = _cameraMovement.To.position,
            duration = 1f,
        }; 
        
        var burgerData = new MovingUtility.MovingContainer()
        {
            originPos = _burgerMovement.From.position,
            targetPos = _burgerMovement.To.position,
            duration = 1.2f
        };
        
        StartCoroutine(MovingUtility.MoveTo(cameraData, CameraMove));
        
        yield return StartCoroutine(MovingUtility.MoveTo(burgerData, BurgerMove));
        
        _particles.Play();
        
        var rating = BurgerComparer.Compare(_customerSpawner.Customer.Burger.GetData(), _logic.PlayerBurger.GetData());
        Menu.Instance.SwitchPage(_ui, rating);
    }

    private void CameraMove(Vector3 obj)
    {
        _cameraMovement.Target.position = obj;
    }

    private void BurgerMove(Vector3 burgerPos)
    {
        _burgerMovement.Target.position = burgerPos;
    }

    public void Deactivate(Action callback = null)
    {
        var data = new MovingUtility.MovingContainer()
        {
            originPos = _cameraMovement.To.position,
            targetPos = _cameraMovement.From.position,
            duration = 1f,
        };
        
        StartCoroutine(MovingUtility.MoveTo(data, CameraMove));
    }
}
