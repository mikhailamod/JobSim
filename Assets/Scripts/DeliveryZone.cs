using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    public Material good;
    public Material bad;

    public void checkCup(List<string> ingredients)
    {
        if(ingredients.Contains("espresso")
            && ingredients.Contains("milk")
            && ingredients.Contains("froth"))
        {
            Debug.Log("Correct!");
            gameObject.GetComponent<Renderer>().material = good;
        }
        else
        {
            Debug.Log("Wrong!");
            gameObject.GetComponent<Renderer>().material = bad;
        }
    }
}
