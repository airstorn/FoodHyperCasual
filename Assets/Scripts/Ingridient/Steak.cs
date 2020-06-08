using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;

public class Steak :  SpawnableIngridient, ICookable, IEditable
{
   [SerializeField] private float _readiness;
   [SerializeField] private float _duration;
   [SerializeField] private AnimationCurve _ratingState;

   protected override void Start()
   {
      base.Start();
   }


   public void Cook(float cookState)
   {
      _readiness = cookState;
   }

   public override float GetRating()
   {
      return _ratingState.Evaluate(_readiness / _duration);
   }

   public float GetReadiness()
   {
      return _readiness;
   }

   public float GetTotalDuration()
   {
      return _duration;
   }

   public Transform GetTransform()
   {
      return transform;
   }
}
