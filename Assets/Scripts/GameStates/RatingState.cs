using System;
using System.Collections;
using System.Collections.Generic;
using GameStates;
using UnityEngine;

public class RatingState : MonoBehaviour, IGameState
{
    [SerializeField] private GameLogic _logic;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _cameraNear;
    [SerializeField] private Transform _cameraDefault;
    [SerializeField] private CustomerSpawner _customerSpawner;
    [SerializeField] private GameObject _ui;
    public void Activate(Action activatAction)
    {
        StartCoroutine(CameraMove(_cameraNear, activatAction));
        var rating = BurgerComparer.Compare(_customerSpawner.Customer.Burger.GetData(), _logic.PlayerBurger.GetData());
        Menu.Instance.SwitchPage(_ui, rating);
    }

    public void Deactivate(Action callback = null)
    {
        StartCoroutine(CameraMove(_cameraDefault, callback));
    }

    private IEnumerator CameraMove(Transform target, Action callback)
    {
        float time = 0;
        float elapsed = 0.5f;
        while (time < elapsed)
        {
            _camera.position = Vector3.Lerp(_camera.position, target.position, time / elapsed);
            _camera.rotation = Quaternion.Lerp(_camera.rotation, target.rotation, time / elapsed);
            
            time += Time.deltaTime;
            yield return null;
        }
        
        callback?.Invoke();
    }
}
