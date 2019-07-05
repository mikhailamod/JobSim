using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Machine : MonoBehaviour
{
    public string ingredient;
    public TextMeshProUGUI label;

    public Material normal;
    public Material interact;

    private void Start()
    {
        label.text = ingredient;
    }

    public void InteractColor()
    {
        gameObject.GetComponent<Renderer>().material = interact;
    }

    public void NormalColor()
    {
        gameObject.GetComponent<Renderer>().material = normal;
    }
}
