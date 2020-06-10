using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;

public class LevelHeader : MonoBehaviour, ILevelListener
{
    [SerializeField] private Text _levelText;


    public void SetLevel(int level)
    {
        _levelText.text = "level " + level;
    }
}
