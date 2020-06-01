using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private GameObject _currentSkin;
    private IBurgerViewable _customerBurger;
    private CustomerRequestCreator _creator;


    public void SetRandomSkin()
    {
        int rand = Random.Range(0, _skins.Length);
        _currentSkin?.SetActive(false);
        _currentSkin = _skins[rand];
        _currentSkin.SetActive(true);
    }

    public void ClearRequest()
    {
        _creator.ClearRequest(ref _customerBurger.GetData());
    }

    public void CreateBurger()
    {
        var data = _customerBurger.GetData();
        RotateBurger(Quaternion.identity);
        _creator.CreateRequest(ref data, CustomerRequestCreator.Difficulty.Easy);
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
            
           yield return MovingUtility.MoveTo(data, delegate(Vector3 pos) { objs[i].GetTransform().position = pos; });
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

    private void RotateBurger(Quaternion rot)
    {
        _burgerOffset.rotation = rot;
    }
    
    public void SetVisible(bool visible)
    {
        _anim.SetBool("visible", visible);
    }
    
    private void Awake()
    {
        _customerBurger = GetComponent<IBurgerViewable>();
        _creator = GetComponent<CustomerRequestCreator>();
    }
}
