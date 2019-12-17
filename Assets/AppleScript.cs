using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleScript : MonoBehaviour
{
    public float feedHealth;
    Movement player = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.transform.tag);
        if (collision.transform.tag.Equals("Player"))
        {
            player = collision.gameObject.GetComponent<Movement>();
            if(player.Health < player.maxHealth)
            {
                player.Health += feedHealth;
                Destroy(this.gameObject);
            }
        }
    }
}
