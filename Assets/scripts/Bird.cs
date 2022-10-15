using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    private bool startingGame = true;
    
    public GameObject gameManager;
    
    public float upVelocity;

    private Rigidbody2D rb;

    public GameObject woodCreator;

    public float gravityScale;

    public GameObject pressSpaceTxt;

    private bool isDead = false;
    
    // bird rotate

    public float maxAngle = 30;
    public float maxHeight = 5;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.eulerAngles = new Vector3(0, 0, 90);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (! IsPointerOverUIObject()) // ensure to enter the condition only if the click position is not within the pause-button area 
            {
                if (isDead) { SceneManager.LoadScene(0); }
                if (pressSpaceTxt.activeSelf) {pressSpaceTxt.SetActive(false);}

                if (startingGame)
                {
                    woodCreator.SetActive(true);
                    rb.gravityScale = gravityScale;
                    gameManager.GetComponent<Speed>().EnableAddDelay();
                    startingGame = false;
                }

                // ensure that when paused: clicking the screen does not trigger a new jump
                if (! Buttons.paused) { Jump(); }
                else { Buttons.Pause(); }
            }
        }

        // update rotation 
        transform.eulerAngles = new Vector3(0, 0, transform.position.y * maxAngle / maxHeight);
    }

    private void Jump()
    {
        rb.velocity = Vector2.up * upVelocity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "fruit(Clone)")
        {
            GameObject.Find("fruitSound").GetComponent<AudioSource>().Play();
            return;
        }
        
        if (other.gameObject.name == "PipePasser")
        {
            return;
        }

        isDead = true;
        // lose sound
        GetComponent<AudioSource>().Play();
        // game over display
        gameManager.GetComponent<GameManager>().GameOver();
    }
    
    private bool IsPointerOverUIObject()
    {
        var eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        };
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
