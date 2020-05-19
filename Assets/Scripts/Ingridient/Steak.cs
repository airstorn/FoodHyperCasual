﻿using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;

public class Steak :  SpawnableIngridient, ICookable
{
   [SerializeField] private float _readiness;
   [SerializeField] private float _duration;

   public void Cook(float cookState)
   {
      _readiness = cookState;
   }

   public float GetReadiness()
   {
      return _readiness;
   }

   public float GetTotalDuration()
   {
      return _duration;
   }
}
