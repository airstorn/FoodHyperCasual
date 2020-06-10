using System;
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
        _handlers  = FindObjectsOfType<MonoBehaviour>().OfType<ILevelListener>().ToArray();

        foreach (var listener in _handlers)
        {
            OnLevelChanged += listener.SetLevel;
        }
        
        SetLevel(LoadLevel());
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
