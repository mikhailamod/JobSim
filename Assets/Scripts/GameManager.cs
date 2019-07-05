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
    int targetTime = 15;
    int[] levelList = { 15, 7, 6, 3, 2 };

    public static bool gameActive = false;
    public bool hasStarted = false;

    //UI stuff
    public TextMeshProUGUI allOrdersText;
    public TextMeshProUGUI allOrdersHeading;

    public TextMeshProUGUI currentOrderHeading;
    public TextMeshProUGUI currentOrderDetails;

    public TextMeshProUGUI correctOrdersText;
    public TextMeshProUGUI timeLeftText;

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

        string headings = "Welcome";
        string bodies = "Pull trigger to begin";

        currentOrderHeading.text = headings;
        currentOrderDetails.text = bodies;

        allOrdersHeading.text = headings;
        allOrdersText.text = bodies;
        
    }

    void ResetAll()
    {
        level = 0;
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
        if (gameActive && hasStarted)
        {
            gameTimer += Time.deltaTime;
            int seconds = Convert.ToInt32(gameTimer);
            timeLeftText.text = "Time: " + seconds + "\nLevel: " + (level+1);
            if (seconds > prevSec)
            {
                if (seconds == targetTime) // generate new order every levelList[level] second  
                {
                    GenerateOrder();
                    Debug.Log("New order generated");
                    targetTime += levelList[level];
                }
                prevSec = seconds;

                if ((seconds % 30) == 0)// change level every 30 seconds
                { 
                    if (level < 4) { level++; }
                }
            }
        }
    }

    public void StartGame()
    {
        hasStarted = true;

        currentOrderHeading.text = "Awaiting Order";
        currentOrderDetails.text = "";

        allOrdersHeading.text = "Orders";
        allOrdersText.text = "";

        GenerateOrder();
        GenerateOrder();
        NextOrder();
    }

    public void NextOrder()
    {
        if(orders.Count > 0)
        {
            currentOrder = orders.Peek();
        }
        currentOrderNum++;
        DisplayOrderList();
    }

    void RemoveCurrentOrder()
    {
        if(orders.Count > 0)
        {
            orders.Dequeue();
            NextOrder();
        }      
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
            currentOrder = orders.Peek();
            DisplayOrderList();
        }
        else GameOver();
    }

    public bool OrderExists() { return (orders.Count > 0); }

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
        correctOrdersText.text = "Correct Orders: " + correctOrders;
        return true;
    }

    public void DisplayOrderList()
    {
        if(orders.Count == 0)
        {
            allOrdersText.text = "";
            currentOrderHeading.text = "Awaiting Order";
            currentOrderDetails.text = "";
            return;
        }
        int count = currentOrderNum;
        allOrdersText.text = "";
        foreach (var item in orders)
        {
            allOrdersText.text += (count + ": " + item + "\n");
            count++;
        }
        currentOrderHeading.text = currentOrder + " Recipe";
        currentOrderDetails.text = "";
        string[] currentIngredients = coffeeType[currentOrder];
        foreach (string ing in currentIngredients)
        {
            currentOrderDetails.text += ("- " + ing + "\n");
        }
    }

    public void GameOver()
    {
        int highScore = PlayerPrefs.GetInt("correctOrders", 0);
        highScore = (correctOrders > highScore) ? correctOrders : highScore;
        PlayerPrefs.SetInt("correctOrders", highScore);

        gameActive = false;
        string go = "Game Over";
        string stats = "Correct orders: " + correctOrders +"\n" +
                                    "High score: " + highScore + "\n" + 
                                    "Time taken: " + (Convert.ToInt32(gameTimer));

        currentOrderHeading.text = go;
        currentOrderDetails.text = stats;

        allOrdersHeading.text = go;
        allOrdersText.text = stats;
    }
}
