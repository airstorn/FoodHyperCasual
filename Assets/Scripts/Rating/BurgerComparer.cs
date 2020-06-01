using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BurgerComparer 
{
    public static float Compare(BurgerData original, BurgerData comparable)
    {
        float status = 0;

        if (comparable._ingridients.Count == original._ingridients.Count)
        {
            for (int i = 0; i < original._ingridients.Count; i++)
            {
                Debug.Log(original._ingridients[i].GetType() + " - " + comparable._ingridients[i]);
                if (original._ingridients[i].GetType() == comparable._ingridients[i].GetType())
                {
                    status += 1f / comparable._ingridients.Count;
                }
            }
        }

        return status;
    }
}
