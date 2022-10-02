using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    public float initSpeed = 3.12f;
    public static float currSpeed;

    private bool add = false;

    public float speedTime;
    
    public float acceleratingAmount;

    public float speedLimit;

    private void Awake()
    {
        currSpeed = initSpeed;
        Invoke("EnableAdd", speedTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (add)
        {
            WoodsCreator.NextStage();
            var newSpeed = currSpeed + acceleratingAmount;
            if (newSpeed <= speedLimit)
            {
                currSpeed = newSpeed;
                add = false;
                Invoke("EnableAdd", speedTime);
                // closer trees :
                GameObject.Find("wood creator").GetComponent<WoodsCreator>().CloserTrees();
            }
        }
    }
    
    private void EnableAdd()
    {
        add = true;

        WoodsCreator.Stage++;
        // todo bug: Stage increases twice 
        // print(WoodsCreator.Stage);
    }
}
