using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class Speed : MonoBehaviour
{
    public static int stagesNum;
    
    public float initSpeed = 3.12f;
    public static float currSpeed;

    private bool add = false;

    public float speedTime;
    
    public float acceleratingAmount;

    public float speedLimit;

    public List<float> speeds;

    public AudioSource acceleratingSound;

    private void Awake()
    {
        stagesNum = 1 + (int)Math.Ceiling((speedLimit - initSpeed) / acceleratingAmount);
        FillSpeeds();
        currSpeed = speeds[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (add)
        {
            WoodsCreator.NextStage();
            currSpeed = speeds[WoodsCreator.Stage];
            add = false;
            Invoke("EnableAdd", speedTime);
            // closer trees :
            // GameObject.Find("wood creator").GetComponent<WoodsCreator>().CloserTrees();

            // WoodsCreator.NextStage();
            // var newSpeed = currSpeed + acceleratingAmount;
            // if (newSpeed <= speedLimit)
            // {
            //     currSpeed = newSpeed;
            //     add = false;
            //     Invoke("EnableAdd", speedTime);
            //     // closer trees :
            //     GameObject.Find("wood creator").GetComponent<WoodsCreator>().CloserTrees();
            // }
        }
    }
    
    private void EnableAdd()
    {
        add = true;
        // accelerating sound:
        acceleratingSound.Play();
    }
    
    public void EnableAddDelay()
    {
        Invoke("EnableAdd", speedTime);
    }

    private void FillSpeeds()
    {
        speeds.Add(initSpeed);
        for (int i = 0; i < stagesNum - 1; i++)
        {
            speeds.Add(speeds[i] + acceleratingAmount);
        }
    }
}
