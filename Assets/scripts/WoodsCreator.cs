using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using SysRandom = System.Random;

public class WoodsCreator : MonoBehaviour
{
    // level managing
    public static int Stage; // the current game stage 
    public List<float> heights; // height range of woods (with middle of 0 - the WoodCreator's transform)
    public List<float> widths; // height range of woods (with middle of 1 - the initial scale)

    // private int acceleratingTime; // every acceleratingTime seconds, woods will bo moving faster
    
    private float maxTime; // distance between woods
    public float averageTime; // distance between woods
    public float deltaTime; // distance between woods
    public float CloserTreesPercentage;
    private float timer = 0;
    public float height; // height range of woods (with middle of 0 - the WoodCreator's transform)
    public float width; // height range of woods (with middle of 1 - the initial scale)

    public GameObject woodPrefab;
    
    // fruits

    public GameObject fruit;

    private List<Sprite> fruitList = new List<Sprite>();
    
    private float maxTimeFruit;

    private float timerFruit = 0;

    private bool fruitIsAllowed = true;

    private bool fruitFlag; // determines if fruit will appear for each wood

    public int fruitFrequency;

    // public Sprite apple;
    // public Sprite strawberry;
    // public Sprite bananas;
    // public Sprite watermelon;
    // public Sprite lemon;
    
    // all fruits

    public List<Sprite> fruits;

    // random
    private SysRandom rnd = new SysRandom();


    private void Start()
    {
        // maxTime = Random.Range(averageTime - deltaTime, averageTime + deltaTime);
        maxTime = -1;
        
        // fruitFlag = rnd.Next(1, 5) > 1; // Probabilistic bias to appearance of fruits: 4/5
        
        // Probabilistic bias to appearance of fruits: fruitFrequency-1 / fruitFrequency
        fruitFlag = rnd.Next(1, fruitFrequency) > 1;

        // FillFruitList();
        
        // Invoke("AccelerateWoods", acceleratingTime);

        Stage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > maxTime)
        {
            // create new wood at the right corner of the screen
            GameObject newWood = Instantiate(woodPrefab);
            // randomize the wood's height and width
            newWood.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0);
            newWood.transform.localScale = new Vector3(Random.Range(1 - width, 1 + width), 1, 1);
            // destroy wood after 15 sec'
            Destroy(newWood, 15);
            // reset timers
            timer = 0;
            timerFruit = 0;
            // randomize the next maxTimes
            maxTime = Random.Range(averageTime - deltaTime, averageTime + deltaTime);
            // determine if fruit will appear in the next time
            fruitFlag = rnd.Next(1, fruitFrequency) > 1;
            // enable fruit generating
            fruitIsAllowed = true;
        }

        // generate fruits exactly at the middle between adjacent woods
        if (fruitFlag && timerFruit > 0.5f * maxTime && fruitIsAllowed)
        {
            // create new fruit at the right corner of the screen
            GameObject newFruit = Instantiate(fruit);
            // randomize height and fruit kind
            newFruit.transform.position = transform.position + new Vector3(0, Random.Range(-height * 2, height * 2), 0);
            // newFruit.GetComponent<SpriteRenderer>().sprite = fruitList[rnd.Next(0, 4)];
            newFruit.GetComponent<SpriteRenderer>().sprite = fruits[rnd.Next(0, fruits.Count - 1)];
            // destroy wood fruit 15 sec'
            Destroy(newFruit, 15);
            // disable fruit generating
            fruitIsAllowed = false;
        }
        
        // update timers
        timer += Time.deltaTime;
        timerFruit += Time.deltaTime;
    }

    // private void FillFruitList()
    // {
    //     fruitList.Add(apple);
    //     fruitList.Add(bananas);
    //     fruitList.Add(watermelon);
    //     fruitList.Add(lemon);
    //     fruitList.Add(strawberry);
    // }

    // private void AccelerateWoods()
    // {
    //     woodPrefab.GetComponent<WoodMove>().WoodsMoveFaster();
    //     Invoke("AccelerateWoods", acceleratingTime);
    // }

    public void CloserTrees()
    {
        averageTime *= CloserTreesPercentage;
    }
}
