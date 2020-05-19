using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SauceParticleTracker : MonoBehaviour
{
   [SerializeField] private ParticleSystem _trackedSystem;

   private List<ParticleSystem.Particle> _enter = new List<ParticleSystem.Particle>();

   public Action<int> OnParticleEnter;

   private void OnParticleTrigger()
   { 
      int count = _trackedSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, _enter);
      if(count > 0)
         OnParticleEnter?.Invoke(count);
      
   }
}
