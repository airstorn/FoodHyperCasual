using System;
using System.Collections;
using System.Collections.Generic;
using Ingridient;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
   [SerializeField] private List<IngridientZoneBase> _spawnZones = new List<IngridientZoneBase>();
   [SerializeField] private GameObject _bun;
   private void Start()
   {
      var burger = new Burger(new List<IIngridient>(new []{ new Onion(), new Onion(), }));
      SpawnElements(burger);
   }

   public void SpawnElements(Burger burger)
   {
      for (int i = 0; i < burger.Ingridients.Count; i++)
      {
         int spawnPoint = Random.Range(0, _spawnZones.Count);
         while (_spawnZones[i].IsSpawned() == true)
         {
            spawnPoint = Random.Range(0, _spawnZones.Count);
         }
         _spawnZones[spawnPoint].Spawn(burger.Ingridients[i].GetSpawnable());
      }
   }
}
