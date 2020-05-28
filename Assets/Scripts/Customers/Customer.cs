using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Transform _burgerOffset;
    [SerializeField] private float _rotationSpeed;
    public IBurgerViewable Burger => _customerBurger;
    
    private IBurgerViewable _customerBurger;
    private CustomerRequestCreator _creator;
    
    


    private void Awake()
    {
        _customerBurger = GetComponent<IBurgerViewable>();
        _creator = GetComponent<CustomerRequestCreator>();
    }

    public void CreateBurger()
    {
        var data = _customerBurger.GetData();

        _creator.CreateRequest(ref data, CustomerRequestCreator.Difficulty.Easy);
    }

    public IEnumerator BurgerRotation(float Xrotation)
    {
        float time = 0;
        while (time < _rotationSpeed)
        {
            _burgerOffset.rotation = Quaternion.Lerp(_burgerOffset.rotation, Quaternion.Euler(Xrotation, 0, 0),
                time / _rotationSpeed);
            
            time += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator AnimateRequest()
    {
        float time = 0;
        float elapsed = 1;
        var objs = _customerBurger.GetData()._ingridients.Select(obj => (IEditable)obj).ToArray();

        while (time < elapsed)
        {
            for (int i = 0; i < objs.Length; i++)
            {
                Transform objectTransform = objs[i].GetTransform();
                
                Vector3 xPos = new Vector3(Mathf.Lerp(-0.7f, 0.7f, ((float)i / (objs.Length - 1)) * (time / elapsed)),
                        Mathf.Lerp(_burgerOffset.position.y + -0.5f,  _burgerOffset.position.y + 0.4f, ((float)i / (objs.Length - 1)) * (time / elapsed)),
                    objectTransform.position.z)
                    ;


                objectTransform.position = Vector3.Lerp(objectTransform.position, xPos, time / elapsed);
            }
            
            time += Time.deltaTime;
            yield return null;
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
    
    public void SetVisible(bool visible)
    {
        _anim.SetBool("visible", visible);
    }
    
}
