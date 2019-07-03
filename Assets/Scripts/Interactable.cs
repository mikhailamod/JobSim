using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class Interactable : MonoBehaviour
{
    [HideInInspector]
    public Hand activeHand = null;
    public bool isSmall = false;

    public List<string> ingredients = new List<string>();

    public DeliveryZone deliveryZone;

    private bool inZone = false;
    private bool isInSpawnArea = true;
    public Vector3 originalPos;

    //UI
    //public TextMeshProUGUI orderDetails;

    private void Start()
    {
        originalPos = transform.position;
    }

    private void Update()
    {
        if (inZone && activeHand == null)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Cup collides with machine");
        if (other.gameObject.CompareTag("Machine"))
        {
            string otherIngredient = other.gameObject.GetComponent<Machine>().ingredient;
            if (!ingredients.Contains(otherIngredient))
            {
                ingredients.Add(otherIngredient);
                Debug.Log("Adding " + otherIngredient + " to cup");
            }
        }

        else if(other.gameObject.CompareTag("Delivery"))
        {
            Debug.Log("Delivery Zone");
            deliveryZone.checkCup(ingredients);
            inZone = true;
        }

        else if(other.gameObject.CompareTag("Spawn"))
        {
            isInSpawnArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Spawn"))
        {
            isInSpawnArea = false;
        }
    }

    public bool IsInSpawn() { return isInSpawnArea; }
}
