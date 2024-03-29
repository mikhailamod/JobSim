﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    public Material good;
    public Material bad;
    public Material normal;

    public void CheckCup(List<string> ingredients)
    {
        if(GameManager.Instance.OrderExists())
        {
            if (GameManager.Instance.CheckOrder(ingredients))
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

    public void ChangeToNormalColor()
    {
        gameObject.GetComponent<Renderer>().material = normal;
    }
}
