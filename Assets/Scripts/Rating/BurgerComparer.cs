using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ingridient;
using UnityEngine;

public static class BurgerComparer 
{
    public static float Compare(BurgerData original, BurgerData comparable)
    {
        float status = 0;

        var ratables = comparable._ingridients.Select(ingridient => ingridient as IRatable).Where(ratable => ratable != null).ToArray();
        
        
        if (comparable._ingridients.Count == original._ingridients.Count)
        {
            for (int i = 1; i < original._ingridients.Count; i++)
            {
                var comparableRatable = comparable._ingridients[i] as IRatable;
                
                if (original._ingridients[i].GetType() == comparable._ingridients[i].GetType())
                {
                    if (comparableRatable != null)
                        status += (comparableRatable.GetRating() / ratables.Length);
                }
            }
        }
        else if (comparable._ingridients.Count < original._ingridients.Count)
        {
            for (int i = 1; i < comparable._ingridients.Count; i++)
            {
                var comparableRatable = comparable._ingridients[i] as IRatable;
                
                if (original._ingridients[i].GetType() == comparable._ingridients[i].GetType())
                {
                    if (comparableRatable != null)
                        status += (comparableRatable.GetRating() / ratables.Length);
                }
            }
        }
        else if (comparable._ingridients.Count > original._ingridients.Count)
        {
            
        }
        
        return status;
    }
}
