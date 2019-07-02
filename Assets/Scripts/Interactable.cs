using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Interactable : MonoBehaviour
{
    [HideInInspector]
    public Hand activeHand = null;

    public List<string> ingredients = new List<string>();

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
            other.gameObject.GetComponent<DeliveryZone>().checkCup(ingredients);
        }
    }
}
