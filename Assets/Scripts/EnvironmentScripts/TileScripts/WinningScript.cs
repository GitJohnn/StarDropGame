using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningScript : MonoBehaviour
{
    bool gameComplete = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            gameComplete = true;
        }
    }
    
    public bool GameComplete
    {
        get
        {
            return gameComplete;
        }
    }

}
