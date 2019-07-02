using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    //public Material good;
    //public Material bad;

    public void checkCup(List<string> ingredients)
    {
        if(ingredients.Contains("espresso")
            && ingredients.Contains("milk")
            && ingredients.Contains("froth"))
        {
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Wrong!");
        }
    }
}
