using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndState : MonoBehaviour, IGameState
{
    private LevelManager _levelManager;

    
    public void Activate(Action activatAction)
    {
        StartCoroutine(ActivationAnimate(activatAction));
    }

    public void Activate<T>(Action activatAction, T arg)
    {
        _levelManager.SetLevel(_levelManager.CurrentLevel + 1);

    }

    private IEnumerator ActivationAnimate(Action callback)
    {
        yield return null;
        Debug.Log("level end");
        callback?.Invoke();
    }

    public void Deactivate(Action callback = null)
    {
    }
    
    private void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
    }
}
