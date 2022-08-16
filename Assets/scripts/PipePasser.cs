using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePasser : MonoBehaviour
{
    public int passScore;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Score.score += passScore;
    }
}
