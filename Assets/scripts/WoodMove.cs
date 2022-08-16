using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodMove : MonoBehaviour
{
    private float speed = 3.12f;

    public float acceleratingAmount;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Speed.currSpeed * Time.deltaTime;
    }

    public void WoodsMoveFaster()
    {
        speed += acceleratingAmount;
    }
}
