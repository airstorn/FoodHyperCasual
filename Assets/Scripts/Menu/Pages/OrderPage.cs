﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OrderPage : PageBasement, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform _orderObject;
    [SerializeField] private Text _orderMessage;
    [SerializeField] private MovingCorner[] _corners = new MovingCorner[3];

    private Animator _anim;
    private Image _orderImage;
    private bool _click;
    private Vector2 _origin;
    private Vector3 _clickPoint;
    private MovingCorner _currentCorner;
    private bool _interactable = true;

    [Serializable]
    private struct MovingCorner
    {
        public float Offset;
        public Color Color;
        public string MessageText;
        public Action OnObjectInCorner;
        public Action<string> OnObjectHoverMessage;
    }

    private void Awake()
    {
        _origin = _orderObject.localPosition;
        _orderImage = _orderObject.GetComponent<Image>();
        _anim = _orderObject.GetComponent<Animator>();

        _corners[0].OnObjectHoverMessage += DisplayHeader;
        _corners[1].OnObjectHoverMessage += DisplayHeader;
        _corners[2].OnObjectHoverMessage += DisplayHeader;
    }

    private void DisplayHeader(string message)
    {
        _orderMessage.text = message;
    }

    public override void Show<T>(T args)
    {
        base.Show(args);
        _anim.SetTrigger("start");
        _interactable = true;
    }

 

    public void OnPointerDown(PointerEventData eventData)
    {
        if(_interactable == false)
            return;
        
        _click = true;
        _clickPoint = Input.mousePosition;
        SetCurrentCorner(_corners[1]);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(_interactable == false)
            return;
        
        _click = false;
        _currentCorner.OnObjectInCorner?.Invoke();
    }

    public void SetOrder(Action decline = null, Action accept = null)
    {
        _orderObject.gameObject.SetActive(true);

        _orderObject.localPosition = _origin;
        _orderImage.color = _corners[1].Color;
        
        _corners[0].OnObjectInCorner = decline;
        _corners[2].OnObjectInCorner = accept;

        _corners[0].OnObjectInCorner += Hide;
        _corners[2].OnObjectInCorner += Hide;
    }

    public override void Hide()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(HideAnim(_currentCorner.Offset > 0));
        }
    }

    private IEnumerator HideAnim(bool rightSided)
    {
            
        var direction = (new Vector3((rightSided ? Screen.width + _orderObject.rect.width / 2 : 0 - _orderObject.rect.width * 1.5f) , _orderObject.localPosition.y, 0));
        
        _interactable = false;
        var move = new MovingUtility.MovingContainer()
        {
            Duration = 1,
            OriginPos = _orderObject.localPosition,
            TargetPos = direction 
        };

        yield return MovingUtility.MoveTo(move, MoveOrder, DisablePage);
    }

    private void DisablePage()
    {
        gameObject.SetActive(false);
    }

    private void MoveOrder(Vector3 vec)
    {
        _orderObject.localPosition = vec;
    }


    private void Update()
    {
        if(_interactable == false)
            return;
        
        if(_click)
            _orderObject.localPosition = new Vector3(-(_clickPoint.x - Input.mousePosition.x), _orderObject.localPosition.y);
        else
            _orderObject.localPosition = Vector3.Lerp(_orderObject.localPosition, _origin, Time.deltaTime * 5);

        LerpRange();
    }

    private void LerpRange()
    {
        for (int i = 0; i < _corners.Length - 1; i++)
        {
            if (_corners[i].Offset < _orderObject.localPosition.x && _corners[i + 1].Offset > _orderObject.localPosition.x)
            {
                var c1 = _corners[i];
                var c2 = _corners[i + 1];
                float lerpedValue = 0;

                if (c2.Offset > 0)
                {
                    lerpedValue = Mathf.Lerp(0, 1, _orderObject.localPosition.x / c2.Offset);

                    if (lerpedValue > 0.8f)
                        SetCurrentCorner(c2);
                    else
                        SetCurrentCorner(c1);
                }
                else
                {
                    lerpedValue = Mathf.Lerp(1, 0, _orderObject.localPosition.x / c1.Offset);
                    
                    if(lerpedValue <   0.2f)
                        SetCurrentCorner(c1);
                    else
                        SetCurrentCorner(c2);
                }

                _orderImage.color = Color.LerpUnclamped(c1.Color, c2.Color, lerpedValue);
            }
        }
    }

    private void SetCurrentCorner(MovingCorner corner)
    {
        _currentCorner = corner;
        corner.OnObjectHoverMessage?.Invoke(corner.MessageText);
    }
}