using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseButton : MonoBehaviour
{
    public Sprite pause; // 0
    public Sprite play; // 1
    
    private int flag = 0;

    private void Awake()
    {
        // GetComponent<UnityEngine.UI.Image>().sprite = play;
    }

    public void SwitchSprites()
    {
        if (flag == 0)
        {
            GetComponent<UnityEngine.UI.Image>().sprite = play;
            flag = 1;
        }
        else
        {
            GetComponent<UnityEngine.UI.Image>().sprite = pause;
            flag = 0;
        }
    }
}
