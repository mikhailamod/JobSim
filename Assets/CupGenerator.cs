using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupGenerator : MonoBehaviour
{

    public GameObject cup;
    public GameObject smallCup;
    
    public void generateCup(bool small, Transform transform)
    {
        //Vector3 temp = new Vector3(0.1f, 0.2f, 0.1f);
        //transform.localScale = temp;
        if(!small) { Instantiate(cup, transform, false); }
        else { Instantiate( smallCup, transform, false); }
    }
}
