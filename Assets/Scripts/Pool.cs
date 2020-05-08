using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ingridient;
using UnityEngine;



public class Pool : MonoBehaviour
{
    [SerializeField] private PoolElement[] _pools;

    public static Pool Instance;

    private void Awake()
    {
        Instance = this;
        PoolElements();

    }

    public ISpawnable GetObject<T>(T type) where T : ISpawnable
    {
        Debug.Log(type.GetType());
        return _pools.First(el =>
        {
            Debug.Log(el.TemplateType);
            return el.TemplateType.GetType() == type.GetType();
        }).GetAvailible();
    }

    private void PoolElements()
    {
        for (int i = 0; i < _pools.Length; i++)
        {
            _pools[i].CreatePrefabs();
        }
    }

    private void OnValidate()
    {
        for (int i = 0; i < _pools.Length; i++)
        {
            _pools[i].Name = _pools[i].Template.name;
        }
    }

    [System.Serializable]
    private struct PoolElement
    {
        [HideInInspector]
        public string Name;
        public GameObject Template => _template;
        public ISpawnable TemplateType;

        [SerializeField] private GameObject _template;
        [SerializeField] private int _poolCount;
        [SerializeField] private ISpawnable[] _prefabs;


        public void CreatePrefabs()
        {
            TemplateType = _template.GetComponent<ISpawnable>();
            
            _prefabs = new ISpawnable[_poolCount];
            
            for (int i = 0; i < _poolCount; i++)
            {
                var obj = Instantiate(_template);
                obj.SetActive(false);
                _prefabs[i] = obj.GetComponent<ISpawnable>();
            }
        }

        public ISpawnable GetAvailible()
        {
            ISpawnable spawn = null;
            foreach (var obj in _prefabs)
            {
                if (obj.Active() == false)
                {
                    spawn = obj;
                }
            }

            return spawn;
        }
    }
}
