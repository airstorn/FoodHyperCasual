using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Ingridient;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
   [SerializeField] private List<IngridientSpawnZoneBase> _spawnZones = new List<IngridientSpawnZoneBase>();
   [SerializeField] private GameObject[] objs;
   private List<ISpawnable> _schedule = new List<ISpawnable>();

   public static Spawner Instance;
   private void Awake()
   {
      Instance = this;
   }

   public IEnumerator SpawnElements(params ISpawnable[] ingridients)
   {
      _schedule =  objs.Select(el =>
      {
         var v = Instantiate(el);
         
         v.SetActive(false);
         
         return v.GetComponent<ISpawnable>();
      }).ToList();
      

      for (int i = 0; i < _spawnZones.Count; i++)
      {
         _spawnZones[i].Spawn(_schedule[0]);
         _schedule.RemoveAt(0);
         yield return new WaitForSeconds(0.2f);
      }
      
      if (_schedule.Count != 0)
      {
         GameLogic.Instance.PlayerBurger.IngridientAction += PlaceScheduledIngridient;
      }
   }

   public void ReturnToSchedule(ISpawnable ing)
   {
      foreach (var ingridientZoneBase in _spawnZones)
      {
         if (ingridientZoneBase.GetHoldedSpawnable() == ing)
         {
            ing.Spawn(ingridientZoneBase.transform);
         }
      }
   }

   public void RemoveFromWaiting(ISpawnable spawnable)
   {
      foreach (var spawnZone in _spawnZones)
      {
         if (spawnZone.GetHoldedSpawnable() == spawnable)
         {
            spawnZone.Remove(spawnable);
         }
      }
   }

   public void Clear()
   {
      foreach (var spawnable in _schedule)
      {
         spawnable.Despawn();
      }
      
      _schedule.Clear();
      
      foreach (var spawnZone in _spawnZones)
      { 
         if(spawnZone.IsEmpty() == false) spawnZone.GetHoldedSpawnable().Despawn();
      }
   }
   
   private void OnDestroy()
   {
      GameLogic.Instance.PlayerBurger.IngridientAction -= PlaceScheduledIngridient;
   }

   private Transform PlaceScheduledIngridient(IIngridient obj)
   {
      if (_schedule.Count == 0)
         return null;
      
      for (int i = 0; i < _spawnZones.Count; i++)
      {
         if(_spawnZones[i].GetHoldedSpawnable() == obj)
            _spawnZones[i].Remove(null);
         if (_spawnZones[i].IsEmpty() == true)
         {
            _spawnZones[i].Spawn(_schedule[0]);
            _schedule.RemoveAt(0);
         }
      }

      return null;
   }
}
