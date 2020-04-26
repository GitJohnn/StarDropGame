using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlazerFireScript : MonoBehaviour
{
    float dmg = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("Player"))
        {
            collision.GetComponent<Movement>().Damage(dmg);
        }
    }
}
