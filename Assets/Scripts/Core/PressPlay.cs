using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PressPlay : MonoBehaviour
{
    [SerializeField] private EventSystem _events;
    [SerializeField] private GameObject _pressPlayObject;

    public static Action OnPlay;

    public void Subscribe()
    {
        InputHandler.OnClick += Play;
        _pressPlayObject.SetActive(true);
    }  
    public void Unsubscribe()
    {
        InputHandler.OnClick -= Play;
        _pressPlayObject.SetActive(false);
    }

    private void Play(bool obj)
    {
        if (_events.currentSelectedGameObject == null)
        {
            OnPlay?.Invoke();
            Unsubscribe();
        }
    }
}
