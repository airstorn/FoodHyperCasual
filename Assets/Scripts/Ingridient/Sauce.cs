using System;
using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;

public class Sauce : InputMovableBehaviour, ISpawnable
{
    [SerializeField] private AnimationCurve _interaction;
    [SerializeField] private ParticleSystem _sauceSystem;
    [SerializeField] private SauceParticleTracker _tracker;
    [SerializeField] private int _fillCountTarget = 20;
    [SerializeField] private int _fillCount;
    [SerializeField] private GameObject _sauceIngridient;
    
    protected override void Start()
    {
        _tracker.OnParticleEnter += OnParticleEnter;
        base.Start();
    }

    private void OnDisable()
    {
        _tracker.OnParticleEnter -= OnParticleEnter;
    }

    private void OnParticleEnter(int obj)
    {
        _fillCount += obj;
        if (_fillCount >= _fillCountTarget)
        {
            var ing = Instantiate(_sauceIngridient, Vector3.zero, Quaternion.identity);
            ing.SetActive(true);
            GameLogic.Instance.PlayerBurger.GetData().AddIngridient(ing.GetComponent<IIngridient>());

            _fillCount = 0;
        }
    }

    // public override void OnSelected(bool selected)
    // {
    //     _rotation = selected ? Quaternion.Euler(new Vector3(0,0, 165)) : Quaternion.Euler(Vector3.zero);
    //
    //     if (selected)
    //     {
    //         _sauceSystem.Play();
    //     }
    //     else
    //     {
    //         _sauceSystem.Stop();
    //     }
    //     
    //     base.OnSelected(selected);
    // }
    public void Spawn(Transform origin)
    {
        transform.position = origin.transform.position;
        gameObject.SetActive(true);
    }

    private void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> l = new List<ParticleSystem.Particle>();
        Debug.Log(_sauceSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, l));
    }

    private void Update()
    {
        // if (selected)
        // {
        //     transform.localScale = new Vector3( _interaction.Evaluate(Time.time), 1, 1);
        // }
    }

    public void BackToPool()
    {
        gameObject.SetActive(false);
    }

    public bool Active()
    {
        return gameObject.activeInHierarchy;
    }
}
