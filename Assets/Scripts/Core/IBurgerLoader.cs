using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBurgerLoader
{
    List<BurgerScriptableObject> GetBurgerList(CustomerRequestCreator.Difficulty difficulty);
}
