using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using SysRandom = System.Random;

public class WoodsCreator : MonoBehaviour
{
    // heights: 2 1.7 1.5
    // widths: 0 0 0.5
    // deltaTimes: 0 0.1 0.2
    // averageTimes: 1.4 1.7 2
    // speed (no change): 3.12, then *0.85 every time

    // level managing 
    public static int Stage = 0; // the current game stage 
    public List<float> heights; // height range of woods (with middle of 0 - the WoodCreator's transform)
    public List<float> widths; // height range of woods (with middle of 1 - the initial scale)
    public List<float> deltaTimes; // height range of woods (with middle of 1 - the initial scale)
    public List<float> averageTimes; // height range of woods (with middle of 1 - the initial scale)
    public List<float> fruitHeights; // height range of woods (with middle of 1 - the initial scale)
    
    // private int acceleratingTime; // every acceleratingTime seconds, woods will bo moving faster
    
    // horizontal distance between woods
    private float generationTime; // generationTime = averageTime + deltaTime
    public float averageTime; 
    public float deltaTime; 
    public float CloserTreesPercentage;
    
    private float timer = 0;
    public float height; // height range of woods (with middle of 0 - the WoodCreator's transform)
    public float width; // width range of woods (with middle of 1 - the initial scale)

    public GameObject woodPrefab;
    
    // fruits

    public GameObject fruit;

    private List<Sprite> fruitList = new List<Sprite>();
    
    private float maxTimeFruit;

    private float fruitTimer = 0;

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
        Stage = 0;
        
        // maxTime = Random.Range(averageTime - deltaTime, averageTime + deltaTime);
        generationTime = -1;
        
        // fruitFlag = rnd.Next(1, 5) > 1; // Probabilistic bias to appearance of fruits: 4/5
        
        // Probabilistic bias to appearance of fruits: fruitFrequency-1 / fruitFrequency
        fruitFlag = rnd.Next(1, fruitFrequency) > 1;
        
        // Invoke("AccelerateWoods", acceleratingTime);

        
        // heights = new List<float>() {1.5f, 0, 0};
        // widths = new List<float>() {0.5f, 0, 0};
    }

    // Update is called once per frame
    void Update()
    {
        // todo refactor generation code to be more intuitive  
        
        if (timer > generationTime)
        {
            // create new wood at the right corner of the screen
            GameObject newWood = Instantiate(woodPrefab);
            // randomize the wood's height and width
            newWood.transform.position = 
                transform.position 
                + new Vector3(0, Random.Range(-heights[Stage], heights[Stage]), 0);
            newWood.transform.localScale = 
                new Vector3(Random.Range(1 - widths[Stage], 1 + widths[Stage]), 1, 1);
            // destroy wood after 15 sec'
            Destroy(newWood, 15);
            // reset timers
            timer = 0;
            fruitTimer = 0;
            // randomize the next maxTimes
            generationTime = 
                Random.Range(
                    averageTimes[Stage] - deltaTimes[Stage], 
                    averageTimes[Stage] + deltaTimes[Stage]);
            // determine if fruit will appear in the next time
            fruitFlag = rnd.Next(1, fruitFrequency) > 1;
            // enable fruit generating
            fruitIsAllowed = true;
        }

        // generate fruits exactly at the middle between adjacent woods
        if (fruitFlag && fruitTimer > 0.5f * generationTime && fruitIsAllowed)
        {
            // create new fruit at the right corner of the screen
            GameObject newFruit = Instantiate(fruit);
            // randomize height and fruit kind
            newFruit.transform.position = 
                transform.position 
                + new Vector3(0, Random.Range(-fruitHeights[Stage] , fruitHeights[Stage] ), 0);
            // newFruit.GetComponent<SpriteRenderer>().sprite = fruitList[rnd.Next(0, 4)];
            newFruit.GetComponent<SpriteRenderer>().sprite = fruits[rnd.Next(0, fruits.Count - 1)];
            // destroy fruit after 15 sec'
            Destroy(newFruit, 15);
            // disable fruit generation
            fruitIsAllowed = false;
        }
        
        // update timers
        timer += Time.deltaTime;
        fruitTimer += Time.deltaTime;
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
        // todo separate between the list and the multiplication 
        averageTime *= CloserTreesPercentage;
    }

    public static void NextStage()
    {
        Stage++;
        print(Stage);
    }
}
