using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    public Material good;
    public Material bad;

    public GameManager gameManager;

    public void checkCup(List<string> interactable)
    {
        if(gameManager.CheckOrder(interactable))
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
