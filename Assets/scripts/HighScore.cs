using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public Text highScore;

    public string highScoreText;

    // highScoreIndicator: false= player had not bitten the high score yet in current play. true otherwise
    private bool highScoreIndicator = false; 
    
    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs.SetInt("HighScore", 4);
        highScore.text = highScoreText + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Score.score > PlayerPrefs.GetInt("HighScore", 0))
        {
            // update high score
            PlayerPrefs.SetInt("HighScore", Score.score);
            
            // display new score right away ?
            // highScore.text = highScoreText + PlayerPrefs.GetInt("HighScore", 0).ToString();
            
            // play high score sound
            if (! highScoreIndicator)
            {
                GetComponent<AudioSource>().Play();
                highScoreIndicator = true;
            }
        }
    }
}
