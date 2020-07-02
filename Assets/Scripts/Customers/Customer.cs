using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Customers;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Customer : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Transform _burgerOffset;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Vector2 _size;
    [SerializeField] private GameObject[] _skins;
    public IBurgerViewable Burger => _customerBurger;
    public ICustomerPresenter Presenter => _presenterAnimator;
    public CustomerRequest Request => _request;

   [SerializeField]  private int _scheduleNumber;
    private GameObject _currentSkin;
    private Animator _skinAnimator;
    private IBurgerViewable _customerBurger;
    private ICustomerPresenter _presenterAnimator;
    private CustomerRequestCreator _creator;
    private CustomerRequest _request;

    public struct CustomerRequest
    {
        public BurgerData Burger;
        public int Price;
    }
    
    public enum AnimationType
    {
        Order,
        MoveVertical
    }

    public void RandomizeSkin()
    {
        int rand = Random.Range(0, _skins.Length);
        _currentSkin?.SetActive(false);
        _currentSkin = _skins[rand];
        _skinAnimator = _currentSkin.GetComponent<Animator>();
        _currentSkin.SetActive(true);
    }

    public void ClearRequest()
    {
        _creator.ClearRequest(ref _customerBurger.GetData());
    }

    public void SetScheduleNumber(int number)
    {
        _scheduleNumber = number;
    }
    
    public void CreateRequest()
    {
        var data = _customerBurger.GetData();
        RotateBurger(Quaternion.identity);
        _request = _creator.CreateRequest(ref data, _scheduleNumber);
    }
    
    public IEnumerator AnimateRequest()
    {
        var burgerRot = new MovingUtility.RotationContainer()
        {
            Duration = 0.1f,
            CurrentRotation = _burgerOffset.rotation,
            TargetRotation = new Vector3(-45, 0)
        };
        
        yield return MovingUtility.Rotate(burgerRot, RotateBurger);

        
        var objs = _customerBurger.GetData()._ingridients.Select(obj => (IEditable) obj).ToArray();

        for (int i = 0; i < objs.Length; i++)
        {
            Transform objectTransform = objs[i].GetTransform();

            var position = _burgerOffset.position;

            Vector3 sizedPosition = new Vector3(
                    Mathf.Lerp(position.x + -_size.x, position.x + _size.x, ((float) i / (objs.Length - 1))),
                    Mathf.Lerp(position.y + -_size.y, position.y + _size.y, ((float) i / (objs.Length - 1))),
                    objectTransform.position.z);

            var data = new MovingUtility.MovingContainer()
            {
                Duration = 0.08f,
                OriginPos = objs[i].GetTransform().position,
                TargetPos = sizedPosition
            };

            var rotData = new MovingUtility.RotationContainer()
            {
                Duration = data.Duration,
                CurrentRotation = objs[i].GetTransform().rotation,
                TargetRotation = new Vector3(0, 0, -15)
            };

            
            MovingUtility.MoveTo(data, 
                delegate(Vector3 pos) { objs[i].GetTransform().position = pos; });
            yield return MovingUtility.Rotate(rotData,
                delegate(Quaternion quaternion) { objs[i].GetTransform().localRotation = quaternion; });
        }
    }

    public IEnumerator AnimateSolving()
    {
        float time = 0;
        float elapsed = 0.5f;
        while (time < elapsed)
        {
            _burgerOffset.rotation = Quaternion.Lerp(_burgerOffset.rotation, Quaternion.identity, time / elapsed);
            
            time += Time.deltaTime;
            yield return null;
        }
    }

    
    public void SetAnimation(AnimationType type, bool state)
    {
        switch (type)
        {
            case AnimationType.Order:
                _anim.SetBool(type.ToString(), state);
                break;
            case AnimationType.MoveVertical:
                _skinAnimator.SetFloat("MoveVertical", state == true ? 1 : 0);
                break;
        }
    }

    public void MoveTo(Vector3 direction, Vector3 rotation, float duration, TweenCallback callback = null)
    {
        Sequence movingSequence = DOTween.Sequence();
        movingSequence.Append(transform.DOMove(direction, duration).SetEase(Ease.InQuad));
        movingSequence.Insert(0, transform.DORotate(rotation, duration));
        movingSequence.AppendCallback(MovingEnd);
        if(callback != null) movingSequence.AppendCallback(callback);
        
        SetAnimation(AnimationType.MoveVertical, true);
    }

    private void MovingEnd()
    {
        SetAnimation(AnimationType.MoveVertical, false);
    }

    private void RotateBurger(Quaternion rot)
    {
        _burgerOffset.rotation = rot;
    }
    
    private void Awake()
    {
        _customerBurger = GetComponent<IBurgerViewable>();
        _creator = GetComponent<CustomerRequestCreator>();
        _presenterAnimator = new CustomerPresenter();
    }
}
