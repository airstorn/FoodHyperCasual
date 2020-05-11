using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : MonoBehaviour, IGameState
{
    public void Activate(Action activatAction)
    {
        StartCoroutine(ActivationAnimate(activatAction));
    }

    private IEnumerator ActivationAnimate(Action callback)
    {
        yield return null;
        callback?.Invoke();
    }

    public void Deactivate(Action callback = null)
    {
    }
}
