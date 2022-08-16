using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    public float initSpeed = 3.12f;
    public static float currSpeed;

    private bool add = true;

    public float speedTime;
    
    public float acceleratingAmount;

    public float speedLimit;

    private void Awake()
    {
        currSpeed = initSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (add)
        {
            var newSpeed = currSpeed + acceleratingAmount;
            if (newSpeed <= speedLimit)
            {
                currSpeed = newSpeed;
                add = false;
                Invoke("EnableAdd", speedTime);
            }
        }
    }
    
    private void EnableAdd()
    {
        add = true;
    }
}
