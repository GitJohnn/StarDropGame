using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PausePanel;

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
            PausePanel.SetActive(!PausePanel.activeInHierarchy);
            if(PausePanel.activeInHierarchy)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
        else
        {
            Debug.Log("There is no Pause Panel");
        }
    }
}
