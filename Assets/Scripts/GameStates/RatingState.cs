using System;
using System.Collections;
using System.Collections.Generic;
using GameStates;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class RatingState : MonoBehaviour, IGameState
{
    [SerializeField] private GameLogic _logic;
    [SerializeField] private PlayState _game;
    [SerializeField] private GameObject _ui;
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private MovingRails _cameraMovement;
    [SerializeField] private MovingRails _burgerMovement;

    public struct RatingData
    {
        public int MoneyIncome;
        public float Score;
    }

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
    
    public void Deactivate(Action callback)
    {
        var cameraData = new MovingUtility.MovingContainer()
        {
            OriginPos = _cameraMovement.To.position,
            TargetPos = _cameraMovement.From.position,
            Duration = 1f,
        }; 
        
        var burgerPosData = new MovingUtility.MovingContainer()
        {
            OriginPos = _burgerMovement.To.position,
            TargetPos = _burgerMovement.From.position,
            Duration = 1.2f
        };
        
        var burgerRotData = new MovingUtility.RotationContainer()
        {
            Duration = 0,
            CurrentRotation = _burgerMovement.From.rotation,
            TargetRotation = new Vector3(0, 0)
        };
        
        MovingUtility.MoveTo(cameraData, CameraMove);
        MovingUtility.MoveTo(burgerPosData, BurgerMove);
        MovingUtility.Rotate(burgerRotData, BurgerRotation);
        
        callback?.Invoke();
    }

    private IEnumerator AnimateActivate(Action activatAction)
    { 
        
        var cameraData = new MovingUtility.MovingContainer()
        {
            OriginPos = _cameraMovement.From.position,
            TargetPos = _cameraMovement.To.position,
            Duration = 1f,
        }; 
        
        var burgerPosData = new MovingUtility.MovingContainer()
        {
            OriginPos = _burgerMovement.From.position,
            TargetPos = _burgerMovement.To.position,
            Duration = 1.2f
        };
        
        var burgerRotData = new MovingUtility.RotationContainer()
        {
            Duration = 2,
            CurrentRotation = _burgerMovement.Target.rotation,
            TargetRotation = new Vector3(0, 180)
        };
        
        MovingUtility.MoveTo(cameraData, CameraMove);
        MovingUtility.Rotate(burgerRotData, BurgerRotation);
        
        var rating = BurgerComparer.Compare(_game.CurrentCustomer.Burger.GetData(), _logic.PlayerBurger.GetData());
        
        if(rating >= 0.5f)
            LevelStatus.Instance.SetSuccessfulCustomers(LevelStatus.Instance.SuccessfulCustomers + 1);
        
        RatingData data = new RatingData()
        {
            Score = rating,
            MoneyIncome = (int) (_game.CurrentCustomer.Request.Price * rating)
        };
        
        Menu.Instance.SwitchPage<RatingPage, RatingData>(data);
        
        yield return MovingUtility.MoveTo(burgerPosData, BurgerMove);
        
        _particles.Play();
        activatAction?.Invoke();
        
    }

    private void BurgerRotation(Quaternion obj)
    {
        _burgerMovement.Target.rotation = obj;
    }

    private void CameraMove(Vector3 obj)
    {
        _cameraMovement.Target.position = obj;
    }

    private void BurgerMove(Vector3 burgerPos)
    {
        _burgerMovement.Target.position = burgerPos;
    }

  
}
