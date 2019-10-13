using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject GameOverPanel;
    public GameObject[] GamePanelChilds;
    Animator Panel;
    bool isPaused = false;
    bool isGameOver = false;

    public bool IsPaused
    {
        get
        {
            return isPaused;
        }
    }
    public bool IsGameOver
    {
        get
        {
            return isGameOver;
        }
    }

    private void Awake()
    {
        PausePanel.SetActive(false);
        StartNewGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PausePanelActivate();
        }
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().Health <= 0)
        {
            GameOverActivate();
            RetryLevel();
        }
    }

    void StartNewGame()
    {
        GameOverPanel.SetActive(true);
        Panel = GameOverPanel.GetComponent<Animator>();
        Panel.SetBool("Fade", false);
        foreach (GameObject child in GamePanelChilds)
        {
            child.SetActive(false);
        }
    }

    public void GameOverActivate()
    {
        if (GameOverPanel.activeInHierarchy && !isGameOver)
        {
            isGameOver = true;
            Panel.SetBool("Fade",true);
            foreach (GameObject child in GamePanelChilds)
            {
                child.SetActive(true);
            }
            Rigidbody2D[] obj = GameObject.FindObjectsOfType<Rigidbody2D>();
            Deactivate(obj);
        }        
    }

    public void PausePanelActivate()
    {
        if (PausePanel)
        {
            PausePanel.SetActive(true);
            isPaused = true;
            Rigidbody2D[] obj = GameObject.FindObjectsOfType<Rigidbody2D>();
            Deactivate(obj);
        }
    }

    void Deactivate(Rigidbody2D[] RBArray)
    {
        foreach(Rigidbody2D rb in RBArray)
        {
            if(rb.bodyType != RigidbodyType2D.Static)
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    void Activate(Rigidbody2D[] RBArray)
    {
        foreach (Rigidbody2D rb in RBArray)
        {
            if (rb.bodyType != RigidbodyType2D.Static)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                if(rb.tag.Equals("Player") || rb.tag.Equals("Enemy") || rb.tag.Equals("Obstacle"))
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }

    void RetryLevel()
    {
        if (Panel.GetBool("Fade") && !Panel.IsInTransition(0) && isGameOver && Input.anyKeyDown)
        {
            Debug.Log("Reseting Level");
            SceneManager.LoadScene(1);
        }
        
    }

    public void ResumeBtn()
    {
        PausePanel.SetActive(false);
        isPaused = false;
        Rigidbody2D[] obj = GameObject.FindObjectsOfType<Rigidbody2D>();
        Activate(obj);
    }
}
