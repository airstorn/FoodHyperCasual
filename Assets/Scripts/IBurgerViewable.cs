using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBurgerViewable
{
    void Show(BurgerData data);
    BurgerData GetData();
}
