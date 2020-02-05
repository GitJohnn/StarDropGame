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
    public GameObject WinPanel;
    public GameObject[] GamePanelChilds;
    public WinningScript gameStatus;
    Rigidbody2D[] obj;
    Animator panelAnim;
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

    private void Start()
    {
        PausePanel.SetActive(false);
        WinPanel.SetActive(false);
        panelAnim = GameOverPanel.GetComponent<Animator>();        
        StartNewGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStatus.GameComplete)
        {
            StartCoroutine(WinGameTransition());
        }
        if (Input.GetKeyDown(KeyCode.P) && !gameStatus.GameComplete && !isGameOver)
        {
            PausePanelActivate();
        }
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().Health <= 0)
        {
            GameOverActivate();
            RetryLevel();
        }
        obj = GameObject.FindObjectsOfType<Rigidbody2D>();
    }

    void StartNewGame()
    {
        Deactivate();
        GameOverPanel.SetActive(true);
        panelAnim.SetBool("Fade", false);
        foreach (GameObject child in GamePanelChilds)
        {
            child.SetActive(false);
        }
        StartCoroutine(FadeIsOver());
    }

    public void GameOverActivate()
    {
        if (GameOverPanel.activeInHierarchy && !isGameOver)
        {
            isGameOver = true;
            panelAnim.SetBool("Fade",true);
            StartCoroutine(FadeBegin());
            Deactivate();
        }        
    }

    public void PausePanelActivate()
    {
        if (PausePanel)
        {
            PausePanel.SetActive(true);
            isPaused = true;
            Deactivate();
        }
    }

    void Deactivate()
    {
        if (obj != null)
        {
            foreach (Rigidbody2D rb in obj)
            {
                if(rb != null)
                {
                    if (rb.bodyType != RigidbodyType2D.Static)
                        rb.constraints = RigidbodyConstraints2D.FreezeAll;

                }
            }
        }
        obj = null;
    }

    void Activate()
    {
        obj = GameObject.FindObjectsOfType<Rigidbody2D>();
        if (obj != null)
        {
            foreach (Rigidbody2D rb in obj)
            {
                if(rb != null)
                {
                    if (rb.bodyType != RigidbodyType2D.Static)
                    {
                        rb.constraints = RigidbodyConstraints2D.None;
                        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    }

                }
            }
        }
    }

    void RetryLevel()
    {
        if (panelAnim.GetBool("Fade") && !panelAnim.IsInTransition(0) && isGameOver && Input.anyKeyDown)
        {
            Debug.Log("Reseting Level");
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex,LoadSceneMode.Single);
        }
        
    }

    public void ResumeBtn()
    {
        Debug.Log("Game Unpaused");
        PausePanel.SetActive(false);
        isPaused = false;
        Activate();
    }

    IEnumerator WinGameTransition()
    {
        WinPanel.SetActive(true);
        yield return new WaitForSeconds(2.45f);
        Deactivate();
        SceneManager.LoadScene(3);
    }

    IEnumerator FadeBegin()
    {
        yield return new WaitForSeconds(1.1f);
        foreach (GameObject child in GamePanelChilds)
        {
            child.SetActive(true);
        }
    }

    IEnumerator FadeIsOver()
    {
        yield return new WaitForSeconds(1.1f);
        Activate();
    }
}
