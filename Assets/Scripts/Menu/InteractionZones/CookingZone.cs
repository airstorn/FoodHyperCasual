using System;
using System.Collections;
using System.Collections.Generic;
using Packages.Rider.Editor.UnitTesting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CookingZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Transform _scalableTransform;
    [SerializeField] private GameObject _obj;
    [SerializeField] private Image _cookingState;

    public static OnEnableHandler PrepareZoneHandler;
    public delegate CookingZone OnEnableHandler(bool enabled, ICookable cookable);

    private ICookable _currentCookable;
    private Coroutine _cookRoutine;
    
    private void Awake()
    {
        PrepareZoneHandler = SetCookable;
    }

    private CookingZone SetCookable(bool enabled, ICookable cookable)
    {
        _obj.SetActive(enabled);
        _currentCookable = cookable;
        return this;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter == _obj)
        {        
            Debug.Log(eventData.pointerEnter);

            _scalableTransform.localScale = Vector3.one;
            _cookRoutine = StartCoroutine(Cook(_currentCookable.Cook));
        }
    }

        

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter == _obj)
        {
            _scalableTransform.localScale = Vector3.zero;
            StopCoroutine(_cookRoutine);
        }
    }

    private void SetScale(bool b)
    {
        _scalableTransform.localScale = Vector3.one * (b ? 1 : 0);
    }
    
    private IEnumerator Cook(Action<float> cookableAction)
    {
        float readiness = _currentCookable.GetReadiness();
        float totalTime = _currentCookable.GetTotalDuration();
        while (readiness < totalTime)
        {
            _cookingState.fillAmount = readiness / totalTime;
            
            cookableAction?.Invoke(readiness);
            
            readiness += Time.deltaTime;
            yield return null;
        }
    }
}
