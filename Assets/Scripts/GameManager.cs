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
    int level;
    int prevOrder;
    float gameTimer;
    int correctOrders, totalOrders;
    int prevSec = 0;
    int currentOrderNum;
    int targetTime = 10;
    int[] levelList = { 10, 9, 8, 7, 6 };
    public static bool gameActive = true;

    public TextMeshProUGUI currentOrderHeading;
    public TextMeshProUGUI currentOrderDetails;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        coffeeType = new Dictionary<string, string[]>();
        coffeeType.Add("espresso", new string[1] { "espresso" });
        coffeeType.Add("americano", new string[2] { "espresso", "water" });
        coffeeType.Add("cappuccino", new string[3] { "espresso", "milk", "froth" });
        //
        rand = new System.Random();
        ResetAll();
        currentOrderHeading.text = "Orders";
        GenerateOrder();
        NextOrder();
    }

    void ResetAll()
    {
        level = 4;
        prevOrder = 0;
        gameTimer = 0.0f;
        correctOrders = 0;
        totalOrders = 0;
        currentOrderNum = 0;
        //int count = 1;
        currentOrderHeading.text = "";
        currentOrderDetails.text = "";
        gameActive = true;
    }

    private void Update()
    {
        if (gameActive)
        {
            gameTimer += Time.deltaTime;
            int seconds = Convert.ToInt32(gameTimer);
            if (seconds > prevSec)
            {
                if (seconds == targetTime) // generate new order every levelList[level] second  
                {
                    GenerateOrder();
                    Debug.Log("New order generated");
                    targetTime += levelList[level];
                }
                prevSec = seconds;

                if ((seconds % 60) == 0) // change level every minute
                    level--;
            }
        }
    }

    public void NextOrder()
    {
        currentOrder = orders.Peek();
        currentOrderNum++;
        //UI stuff
        string[] temp = coffeeType[currentOrder];
        DisplayOrderList();
    }

    void RemoveCurrentOrder()
    {
        orders.Dequeue();
        NextOrder();
    }

    void GenerateOrder()
    {
        string[] myKeys = new string[20];
        string[] myValues = new string[20];
        
        int randNum = prevOrder;
        //
        if (orders.Count < 5)
        {
            coffeeType.Keys.CopyTo(myKeys, 0);
            while (prevOrder == randNum)
                randNum = rand.Next(0, 3);
            orders.Enqueue(myKeys[randNum]);
            prevOrder = randNum;
            totalOrders++;
            DisplayOrderList();
        }
        else GameOver();
    }

     public bool CheckOrder(List<string> ingredients)
    {
        string[] currentIngredients = coffeeType[currentOrder];
        foreach (string val in currentIngredients)
        {
            if (!ingredients.Contains(val))
                return false;
        }

        RemoveCurrentOrder();
        correctOrders++;
        return true;
    }

    public void DisplayOrderList()
    {
        int count = currentOrderNum;
        currentOrderDetails.text = "";
        foreach (var item in orders)
        {
            currentOrderDetails.text += (count + " - " + item + "\n");
            count++;
        }
    }

    public void GameOver()
    {
        gameActive = false;
        currentOrderHeading.text = "Game Over";
        currentOrderDetails.text = "Correct orders: " + correctOrders + "/" + totalOrders +"\n" +
            "Time taken: " + (Convert.ToInt32(gameTimer));
    }
}
