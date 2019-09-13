using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudScript : MonoBehaviour
{
    public float mudStickyness = 2f;

    float originalSpeed;
    float enemyOriginalSpeed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player") && !other.GetComponent<Movement>().isJumping)
        {
            originalSpeed = other.GetComponent<Movement>().speed;
            other.GetComponent<Movement>().speed /= mudStickyness;
        }

        if (other.tag.Equals("Enemy"))
        {
            Debug.Log("Eemy entered mud");
            enemyOriginalSpeed = other.GetComponent<EnemyAI>().Speed;
            other.GetComponent<EnemyAI>().Speed /= mudStickyness;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<Movement>().speed = originalSpeed;
        }

        if (other.tag.Equals("Enemy"))
        {
            other.GetComponent<EnemyAI>().Speed = enemyOriginalSpeed;
        }
    }
}
