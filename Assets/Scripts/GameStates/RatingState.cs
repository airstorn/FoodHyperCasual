﻿using System;
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

    private LevelManager _levelManager;

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
        _levelManager.SetLevel(_levelManager.CurrentLevel + 1);
        
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
        
        // var rating = BurgerComparer.Compare(_customerSpawner.Customer.Burger.GetData(), _logic.PlayerBurger.GetData());
        // Menu.Instance.SwitchPage<RatingPage, float>(rating);
        
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

    private void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
    }
}
