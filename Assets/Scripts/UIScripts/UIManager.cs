using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Image healthUI;
    [SerializeField] Image staminaUI;
    [SerializeField] Image grabTimer;
    Movement player;
    Grabber grabber;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        grabber = GameObject.FindGameObjectWithTag("Grabber").GetComponent<Grabber>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        UpdateStamina();
        UpdateGrab();
    }

    void UpdateHealth()
    {
        healthUI.fillAmount = player.Health / player.maxHealth;
    }

    void UpdateStamina()
    {
        staminaUI.fillAmount = player.Stamina / player.maxStamina;
    }

    void UpdateGrab()
    {
        grabTimer.fillAmount = grabber.Grip / grabber.maxGrip;
    }
}
