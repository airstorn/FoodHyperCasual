using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private GameObject _playerBurgerObject;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private bool _debug;
    [SerializeField] private CustomerSpawner _customer;

    public Spawner IngridientSpawner => _spawner;
    public IBurgerViewable PlayerBurger => _playerBurger;
    public IBurgerViewable CustomerRequest => _customerRequest;
    
    private IGameState[] _allStates;
    private IGameState _currentState;

    private IBurgerViewable _playerBurger;
    private IBurgerViewable _customerRequest;
    
    public static GameLogic Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _playerBurger = _playerBurgerObject.GetComponent<IBurgerViewable>();
        _customerRequest = _customer.Customer.Burger;
        
        LoadStates();

        _currentState = _allStates[1];
        _currentState.Activate(ChangeDebug);
    }

    private void LoadStates()
    {
        var childStates = GetComponentsInChildren<IGameState>();

        _allStates = childStates;
    }
    

    public void ChangeState<T>() where T : IGameState
    {
        var state = _currentState;
        _currentState = _allStates.OfType<T>().First();
        state.Deactivate(StateActivate);
    }

    private void StateActivate()
    {
        _currentState.Activate(ChangeDebug);
    }


    private void ChangeDebug()
    {
        if(_debug) Debug.Log(_currentState);
    }
}
