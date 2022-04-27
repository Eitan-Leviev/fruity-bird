using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score = 0; // score var that visible to all others
    
    // Start is called before the first frame update
    void Start()
    {
        score = 0; // cuz static var sustains reloading scenes
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = score.ToString();
    }
}
