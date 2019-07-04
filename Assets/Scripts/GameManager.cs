using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    //references new cups need
    public DeliveryZone deliveryZone;
    public Transform destoryZone;

    //OrderedDictionary coffeeType;
    Dictionary<string, string[]> coffeeType;
    public Queue<string> orders = new Queue<string>();
    public string currentOrder;
    static System.Random rand;

    public TextMeshProUGUI currentOrderHeading;
    public TextMeshProUGUI currentOrderDetails;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
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
        //
        coffeeType.Keys.CopyTo(myKeys, 0);
        for (int i = 0; i < 20; ++i)
        {
            int randNum = rand.Next(0, 3);
            orders.Enqueue( myKeys[randNum] );
        }
    }

     public bool CheckOrder(List<string> ingredients)
    {
        Debug.Log("Cup has: " + ingredients);
        string[] currentIngredients = coffeeType[currentOrder];
        Debug.Log("Required: " + currentIngredients);
        foreach (string val in currentIngredients)
        {
            if (!ingredients.Contains(val))
                return false;
        }

        RemoveCurrentOrder();
        return true;
    }
}
