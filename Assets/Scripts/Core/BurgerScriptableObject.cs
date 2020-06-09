using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ingridient;
using UnityEngine;

[CreateAssetMenu(fileName = "New Burger", menuName = "New Burger")]
public class BurgerScriptableObject : ScriptableObject
{
   public List<IIngridient> BurgerIngridients => _burger.Select(o => o.GetComponent<IIngridient>()).ToList();
   
   [SerializeField] private List<GameObject> _burger;

   private void OnValidate()
   {
      for (int i = 0; i < _burger.Count; i++)
      {
         if (_burger[i].GetComponent<IIngridient>() == null)
         {
            _burger.RemoveAt(i);
            i--;
         }
      }
   }
}
