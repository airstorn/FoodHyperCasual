﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int CurrentLevel => _currentLevel;
    
    private ILevelListener[] _handlers;
    private Action<int> OnLevelChanged;
    private int _currentLevel;

    private readonly string _levelKey = "level";
    
    private void Awake()
    {
        _handlers  = Resources.FindObjectsOfTypeAll<MonoBehaviour>().OfType<ILevelListener>().ToArray();
        SetLevel(LoadLevel());
        
        foreach (var listener in _handlers)
        {
            Subscribe(listener);
        }
        
    }

    public void Subscribe<T>(T handler) where T : ILevelListener
    {
        OnLevelChanged += handler.SetLevel;
        handler.SetLevel(CurrentLevel);
    }

    public void SetLevel(int level)
    {
        _currentLevel = level;
        OnLevelChanged?.Invoke(CurrentLevel);
        SaveLevel();
    }

    private int LoadLevel() 
    {
        return PlayerPrefs.GetInt(_levelKey, 1);
    }

    private void SaveLevel()
    {
        PlayerPrefs.SetInt(_levelKey, CurrentLevel);
    }
}
