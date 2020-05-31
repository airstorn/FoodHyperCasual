using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CookingZoneUI : MonoBehaviour
{
    [SerializeField] private GameObject _obj;
    [SerializeField] private Image _cookingState;

    private ICookable _currentCookable;
    private Coroutine _cookRoutine;

    public void SetCookingState(float duration, float state)
    {
        _cookingState.fillAmount = state / duration;

        _obj.SetActive(true);
    }

    public void StopCooking()
    {
        _obj.SetActive(false);
    }
}
