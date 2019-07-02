using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupGenerator : MonoBehaviour
{

    public GameObject cup;
    public GameObject smallCup;
    
    public void generateCup(bool small, Vector3 pos)
    {
        if(!small)
        {
            GameObject go = Instantiate(cup, this.transform);
            go.transform.position = pos;
        }
        else
        {
            GameObject go = Instantiate( smallCup, this.transform);
            go.transform.position = pos;
        }
    }
}
