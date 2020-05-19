using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ingridient;
using UnityEngine;

public class PlayerBurger : MonoBehaviour, IBurgerViewable
{
    public BurgerData ContainedData;
    public event OnIngridientAdded IngridientAction;
    
    [SerializeField] private Transform _origin;
    [SerializeField] private GameObject _bunObject;
    [SerializeField] private GameObject _secondBunObject;

    private void Start()
    {
        ContainedData.OnIngridientAdded += OnIngridientAdded;
        
        ContainedData._ingridients = new List<IIngridient>();
        PlaceBun(_bunObject);
    }
    public void Confirm()
    {
        PlaceBun(_secondBunObject);
    }

    private void PlaceBun(GameObject obj)
    {
        obj.SetActive(true);
        ContainedData.AddIngridient(obj.GetComponent<IIngridient>());
    }

    private void OnDisable()
    {
        ContainedData.OnIngridientAdded -= OnIngridientAdded;
    }

    private void OnIngridientAdded(IIngridient obj)
    {
        Debug.Log(obj + " added");

        ISelectable select = obj as ISelectable;
        
        var position = transform.position;
        var vacantPos = new Vector3(position.x,
            position.y + ContainedData._ingridients.Sum(el => el != obj ? el.GetHeight() : 0),
                position.z);


        if (@select != null) StartCoroutine(PlaceAnimation(@select, vacantPos, @select.Move));
    }

    private IEnumerator PlaceAnimation(ISelectable obj, Vector3 targetPos, Action<Vector3> movingAction)
    {
        float elapsedtime = 0;
        float duration = 0.3f;
        var objectTransform =  obj.GetPosition();
        
        while (elapsedtime < duration)
        {
            movingAction?.Invoke(Vector3.Lerp(objectTransform, targetPos, elapsedtime / duration));
            obj.Rotate(Vector3.zero);
            
            elapsedtime += Time.deltaTime;
            yield return null;
        }
    }


    public BurgerData GetData()
    {
        return ContainedData;
    }
}
