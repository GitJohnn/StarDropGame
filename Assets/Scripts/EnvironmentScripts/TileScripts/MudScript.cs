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
            originalSpeed = other.GetComponent<Movement>().CurrentSpeed;
            other.GetComponent<Movement>().CurrentSpeed /= mudStickyness;
        }

        if (other.tag.Equals("Enemy"))
        {
            enemyOriginalSpeed = other.GetComponent<EnemyAI>().Speed;
            other.GetComponent<EnemyAI>().Speed /= mudStickyness;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<Movement>().CurrentSpeed = originalSpeed;
        }

        if (other.tag.Equals("Enemy"))
        {
            other.GetComponent<EnemyAI>().Speed = enemyOriginalSpeed;
        }
    }
}
