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

                Vector3 xPos = new Vector3(Mathf.Lerp(-1, 1, ((float)i / objs.Length) * (time / elapsed)),
                    objectTransform.position.y,
                    objectTransform.position.z)
                    ;


                objectTransform.position = Vector3.Lerp(objectTransform.position, xPos, time / elapsed);
            }
            
            time += Time.deltaTime;
            yield return null;
        }
    }
    
    public void SetVisible(bool visible)
    {
        _anim.SetBool("visible", visible);
    }
    
}
