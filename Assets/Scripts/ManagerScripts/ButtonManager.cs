using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject MainPanel;
    [SerializeField] GameObject PlayPanel;
    [SerializeField] GameObject OptionsPanel;

    private void Awake()
    {
        BackBtn();
    }

    public void PlayPanelActivate()
    {
        MainPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        PlayPanel.SetActive(true);
    }

    public void OptionsPanelActivate()
    {
        MainPanel.SetActive(false);
        OptionsPanel.SetActive(true);
        PlayPanel.SetActive(false);
    }

    public void BackBtn()
    {
        MainPanel.SetActive(true);
        OptionsPanel.SetActive(false);
        PlayPanel.SetActive(false);
    }

    public void PlayBtn()
    {
        SceneManager.LoadScene("Level1");
    }
    public void NextLevelBtn()
    {
        SceneManager.LoadScene("Level1");
    }
    public void ReplayBtn()
    {
        SceneManager.LoadScene("Level1");
    }

    public void MenuBtn()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void QuitBtn()
    {
        Application.Quit();
    }

    public void OptionsBtn()
    {
        //idk like sounds? or graphics..
    }

}
