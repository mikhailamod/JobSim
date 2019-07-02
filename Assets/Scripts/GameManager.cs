using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //OrderedDictionary coffeeType;
    Dictionary<string, string[]> coffeeType;
    Queue<string> orders;
    string currentOrder;
    static System.Random rand;
    // Start is called before the first frame update
    void Start()
    {
        coffeeType = new Dictionary<string, string[]>();
        coffeeType.Add("espresso", new string[1] { "espresso" });
        coffeeType.Add("americano", new string[2] { "espresso", "water" });
        coffeeType.Add("cappuccino", new string[3] { "espresso", "milk", "froth" });
        //
        rand = new System.Random();
        GenerateOrders();
        NextOrder();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NextOrder()
    {
        currentOrder = orders.Dequeue();
    }

    void GenerateOrders()
    {
        string[] myKeys = new string[20];
        string[] myValues = new string[20];
        //
        coffeeType.Keys.CopyTo(myKeys, 0);
        for (int i = 0; i < 20; ++i)
        {
            int randNum = rand.Next(0, 3);
            orders.Enqueue( myKeys[randNum] );
        }
    }

    bool CheckOrder(Interactable interactable)
    {
        string[] currentIngredients = coffeeType[currentOrder];
        foreach (string val in interactable.ingredients)
        {
            if (Array.IndexOf(currentIngredients, val) < 0)
                return false;
        }
        return true;
    }
}
