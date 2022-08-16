using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public float speed;

    public int fruitScore;
    
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Score.score += fruitScore;
        gameObject.SetActive(false);
    }
}
