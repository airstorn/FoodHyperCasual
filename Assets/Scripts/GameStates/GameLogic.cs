using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private IGameState[] _allStates;
    private IGameState _currentState;
    
    private void Start()
    {
        GetStates();

        _currentState = _allStates[0];
        _currentState.Activate(ChangeDebug);
    }

    private void GetStates()
    {
        var childStates = GetComponentsInChildren<IGameState>();

        _allStates = childStates;
        Debug.Log((_allStates.Length));
    }

    public void ChangeState<T>(T state) where T : IGameState
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
