using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject canvasGameOver;

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        // activate canvas
        canvasGameOver.SetActive(true);
        // freeze game
        Time.timeScale = 0;
    }

    public void PlayAgain()
    {
        // reload the current scene
        SceneManager.LoadScene(0);
    }
}
