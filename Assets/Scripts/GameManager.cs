using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //OrderedDictionary coffeeType;
    Dictionary<string, string[]> coffeeType;
    public Queue<string> orders = new Queue<string>();
    public string currentOrder;
    static System.Random rand;

    public TextMeshProUGUI currentOrderHeading;
    public TextMeshProUGUI currentOrderDetails;
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

    public void NextOrder()
    {
        currentOrder = orders.Peek();
        //UI stuff
        currentOrderHeading.text = currentOrder;
        string[] temp = coffeeType[currentOrder];
        currentOrderDetails.text = "";
        for(int i = 0; i < temp.Length; i++)
        {
            currentOrderDetails.text += ("- " + temp[i] + "\n");
        }
    }

    void RemoveCurrentOrder()
    {
        orders.Dequeue();
        NextOrder();
    }

    void GenerateOrders()
    {
        string[] myKeys = new string[20];
        string[] myValues = new string[20];
        int prevOrder = 0;
        int randNum = prevOrder;
        //
        coffeeType.Keys.CopyTo(myKeys, 0);
        for (int i = 0; i < 20; ++i)
        {
            while (prevOrder == randNum)
                randNum = rand.Next(0, 3);
            orders.Enqueue( myKeys[randNum] );
            prevOrder = randNum;
        }
    }

     public bool CheckOrder(List<string> ingredients)
    {
        //Debug.Log("Cup has: " + ingredients);
        string[] currentIngredients = coffeeType[currentOrder];
        //Debug.Log("Required: " + currentIngredients);
        foreach (string val in currentIngredients)
        {
            if (!ingredients.Contains(val))
                return false;
        }

        RemoveCurrentOrder();
        return true;
    }
}
