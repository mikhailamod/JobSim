using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Machine : MonoBehaviour
{
    public string ingredient;
    public TextMeshProUGUI label;

    private void Start()
    {
        label.text = ingredient;
    }
}
