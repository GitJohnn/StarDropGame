using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudScript : MonoBehaviour
{

    public float originalSpeed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player") && !other.GetComponent<Movement>().isJumping)
        {
            originalSpeed = other.GetComponent<Movement>().speed;
            other.GetComponent<Movement>().speed /= 2f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag.Equals("Player") && !other.GetComponent<Movement>().isJumping)
        {
            originalSpeed = other.GetComponent<Movement>().speed;
            other.GetComponent<Movement>().speed /= 2f;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<Movement>().speed = originalSpeed;
        }
    }
}
