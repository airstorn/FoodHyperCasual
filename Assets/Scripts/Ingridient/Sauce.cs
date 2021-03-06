﻿using System;
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
    [SerializeField] private float _interactionSpeed = 0.5f;
    [SerializeField] private Coroutine _interactionRoutine;
    [SerializeField] private AnimationCurve _spawnAnim;
    [SerializeField] private AnimationCurve _depawnAnim;

    protected override void Start()
    {
        _tracker.OnParticleEnter += OnParticleEnter;
        OnSelectedAction += Interact;
        base.Start();
    }
    
    public void Spawn(Transform origin)
    {
        transform.position = origin.position;
        transform.rotation = origin.rotation;
        gameObject.SetActive(true);
        
        var spawnSize = new MovingUtility.FloatLerpContainer()
        {
            Duration = 1f,
            StartValue = 0,
            TargetValue = 1
        };
        
        MovingUtility.LerpFloat(spawnSize, SpawnAnimation);
    }

    public void Despawn()
    {
        var despawnAnim = new MovingUtility.FloatLerpContainer()
        {
            Duration = 1f,
            StartValue = 0,
            TargetValue = 1
        };
        
        Spawner.Instance.RemoveFromWaiting(this);
        
        MovingUtility.LerpFloat(despawnAnim, DespawnAnimation, DespawnEnd);
    }

    private void DespawnEnd()
    {
        Destroy(gameObject);
    }
    
    private void DespawnAnimation(float delta)
    {
        transform.localScale = Vector3.one * _depawnAnim.Evaluate(delta);
    }
    
    private void SpawnAnimation(float delta)
    {
        transform.localScale = Vector3.one * _spawnAnim.Evaluate(delta);
    }
    
    private void Interact(bool obj)
    {
        if(_interactionRoutine != null)
            StopCoroutine(_interactionRoutine);
        _interactionRoutine = StartCoroutine(AnimateInteraction(obj));
    }

    private IEnumerator AnimateInteraction(bool state)
    {
        float elapsed = 0;
        
        _tracker.Play(state);
        
        while (elapsed < _interactionSpeed)
        {
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(transform.rotation.eulerAngles), Quaternion.Euler(state == true ? new Vector3(0,0, 150) : Vector3.zero), elapsed / _interactionSpeed);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
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
    

    private void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> l = new List<ParticleSystem.Particle>();
        Debug.Log(_sauceSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, l));
    }

    private void Update()
    {
        if (_selected)
        {
            transform.localScale = new Vector3( _interaction.Evaluate(Time.time), 1, 1);
        }
    }
}
