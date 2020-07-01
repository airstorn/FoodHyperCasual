using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameStates;
using Ingridient;
using UnityEngine;

public class PlayerBurger : MonoBehaviour, IBurgerViewable, IEditable
{
    public BurgerData ContainedData;
    public event OnIngridientAdded IngridientAction;
    
    [SerializeField] private GameObject _bunObject;
    [SerializeField] private GameObject _secondBunObject;

    private void Awake()
    {
        ContainedData.OnIngridientAdded += OnIngridientAdded;
        
        ContainedData._ingridients = new List<IIngridient>();
    }

    private void OnDisable()
    {
        ContainedData.OnIngridientAdded -= OnIngridientAdded;
    }

    private void OnIngridientAdded(IIngridient obj)
    {
        if(obj is ISpawnable spawnable)
            Spawner.Instance.RemoveFromWaiting(spawnable);

        ISelectable select = obj as ISelectable;

        var position = transform.position;
        var vacantPos = new Vector3(position.x,
            position.y + ContainedData._ingridients.Sum(el => el != obj ? el.GetHeight() : 0),
            position.z);
        
        if (select != null)
        {
            if (@select != null) StartCoroutine(PlaceAnimation(@select, vacantPos, @select.Move));
        }
        else
        {
            var t = obj as IEditable;
            t.GetTransform().position = vacantPos;
            t.GetTransform().gameObject.layer = 0;
            t.GetTransform().SetParent(transform);
            
            
        }

        IngridientAction?.Invoke(obj);
    }

    private IEnumerator PlaceAnimation(ISelectable obj, Vector3 targetPos, Action<Vector3> movingAction)
    {
        float elapsedtime = 0;
        float duration = 0.3f;
        var objectTransform =  obj.GetPosition();
        
        while (elapsedtime < duration)
        {
            movingAction?.Invoke(Vector3.Lerp(objectTransform, targetPos, elapsedtime / duration));
            obj.Rotate(new Vector3(0, obj.GetRotation().y, 0));
            
            elapsedtime += Time.deltaTime;
            yield return null;
        }
        
        var t = obj as IEditable;
        t.GetTransform().position = targetPos;
        t.GetTransform().gameObject.layer = 0;
        t.GetTransform().SetParent(transform);
        
        if (obj is UpperBun)
        {
            PlayState.Confirm();
        }
    }


    public ref BurgerData GetData()
    {
        return ref ContainedData;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
