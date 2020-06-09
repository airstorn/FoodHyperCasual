using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerBurgerLoader : MonoBehaviour, IBurgerLoader
{
    
    
    public List<BurgerScriptableObject> GetBurgerList(CustomerRequestCreator.Difficulty difficulty)
    {
        string path = "";
        
        switch (difficulty)
        {
            case CustomerRequestCreator.Difficulty.Easy:
                path = "Burgers/Easy";
                break;
            case CustomerRequestCreator.Difficulty.Medium:
                path = "Burgers/Medium";
                break;
            case CustomerRequestCreator.Difficulty.Hard:
                path = "Burgers/Hard";
                break;
        }

        var data = Resources.LoadAll<BurgerScriptableObject>(path).ToList();

        return data;
    }
}
