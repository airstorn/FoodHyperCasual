using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private GameObject _playerBurgerObject;

    public IBurgerViewable PlayerBurger => _playerBurger;
    
    private IGameState[] _allStates;
    private IGameState _currentState;

    private IBurgerViewable _playerBurger;
    
    public static GameLogic Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _playerBurger = _playerBurgerObject.GetComponent<IBurgerViewable>();
        
        GetStates();

        _currentState = _allStates[1];
        _currentState.Activate(ChangeDebug);
    }

    private void GetStates()
    {
        var childStates = GetComponentsInChildren<IGameState>();

        _allStates = childStates;
        Debug.Log((_allStates.Length));
    }

    public void ChangeState<T>() where T : IGameState
    {
        _currentState.Deactivate();
        _currentState = _allStates.OfType<T>().First();
        _currentState.Activate(ChangeDebug);
    }

    private void ChangeDebug()
    {
        Debug.Log("state switched to " + _currentState);
    }
}
