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

    Transform bulletParent;

    private void Awake()
    {
        bulletParent = transform.parent;
    }

    void Update()
    {
        Shooting();
    }

    void Shooting()
    {        
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        if(timeDestroy >= startTimeDestroy)
        {
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
            if (other.GetComponent<Draggable>()){
                other.GetComponent<Draggable>().durability -= dmg;
            }
                Destroy(gameObject);
        }
        // depends on bullet tag
        if (transform.tag.Equals("EnemyBullet"))
        {
            if (other.tag.Equals("Enemy") && other.transform != bulletParent)
            {
                other.transform.GetComponent<EnemyAI>().Damage(dmg);
                other.transform.GetComponent<EnemyAI>().takeKnockBack(transform.position, knockBackDealt);
            }
            if (other.tag.Equals("Player") && !other.GetComponent<Movement>().isJumping)
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
