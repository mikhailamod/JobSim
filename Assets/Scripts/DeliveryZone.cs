using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    public Material good;
    public Material bad;
    public Material normal;

    public GameManager gameManager;

    public void CheckCup(List<string> interactable)
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

    public void ChangeToNormalColor()
    {
        gameObject.GetComponent<Renderer>().material = normal;
    }
}
