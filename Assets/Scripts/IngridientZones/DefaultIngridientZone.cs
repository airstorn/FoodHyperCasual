using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultIngridientZone : IngridientZoneBase
{
   [SerializeField] private GameObject _selfObject;
   public override void Interact()
   {
      base.Interact();
   }
}
