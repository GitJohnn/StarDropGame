using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float startTimeDestroy = 2f;
    float timeDestroy = 0;
    public float dmg;
    public float knockBackDealt;

    void Update()
    {
        Shooting();
    }

    void Shooting()
    {        
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        if(timeDestroy >= startTimeDestroy)
        {
            //Debug.Log("Bullet destroyed");
            Destroy(gameObject);
        }
        else
        {
            timeDestroy += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Obstacle"))
        {
            Debug.Log("Bullet Destroyed");
            Destroy(gameObject);
        }

        if (transform.tag.Equals("EnemyBullet"))
        {
            if (other.tag.Equals("Player"))
            {
                other.transform.GetComponent<Movement>().Damage(dmg);
                other.transform.GetComponent<Movement>().takeKnockBack(transform.position, knockBackDealt);
                Destroy(gameObject);
            }
        }
        else if(transform.tag.Equals("NormalBullet"))
        {
            if (other.tag.Equals("Enemy"))
            {
                other.transform.GetComponent<EnemyAI>().Damage(dmg);
                other.transform.GetComponent<EnemyAI>().takeKnockBack(transform.position, knockBackDealt);
                Destroy(gameObject);
            }
        }
    }

}
