using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PausePanel;
    public bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PausePanelActivate();
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
        else
        {
            Debug.Log("There is no Pause Panel");
        }
    }

    void Deactivate(Rigidbody2D[] RBArray)
    {
        foreach(Rigidbody2D rb in RBArray)
        {
            if(rb.bodyType != RigidbodyType2D.Static)
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
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

    public void ResumeBtn()
    {
        PausePanel.SetActive(false);
        isPaused = false;
        Rigidbody2D[] obj = GameObject.FindObjectsOfType<Rigidbody2D>();
        Activate(obj);
    }
}
