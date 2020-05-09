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
   [SerializeField] private List<IngridientZoneBase> _spawnZones = new List<IngridientZoneBase>();
   [SerializeField] private GameObject[] objs;

   private void Start()
   {
      StartCoroutine(SpawnElements(objs.Select(el => Instantiate(el).GetComponent<ISpawnable>()).ToArray()));
   }

   public IEnumerator SpawnElements(params ISpawnable[] ingridients)
   {
      for (int i = 0; i < ingridients.Length; i++)
      { 
         int spawnPoint = 0;
         for (int j = 0; j < _spawnZones.Count; j++)
         {
            if (_spawnZones[j].IsSpawned() == false)
            {
               spawnPoint = j;
            }
         }
         _spawnZones[spawnPoint].Spawn(ingridients[i]);
         yield return new WaitForSeconds(0.2f);
      }
   }
}
