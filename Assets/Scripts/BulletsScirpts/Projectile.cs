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
    public bool penetrate;

    Transform bulletParent;
    GameManager manager;
    Rigidbody2D myRB;

    private void Awake()
    {
        bulletParent = transform.parent;
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        myRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Shooting();
    }

    void Shooting()
    {
        if (!transform.tag.Equals("EnemyBullet"))
        {
            //changed the moving method so i could freeze rigidbody on pause
            //myRB.AddForce(transform.right * speed * Time.deltaTime, ForceMode2D.Impulse);
            //myRB.velocity = transform.right * speed;
        }
        else
        {
            //changed the moving method so i could freeze rigidbody on pause
            //myRB.AddForce(transform.right * speed * Time.deltaTime, ForceMode2D.Impulse);
            //transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        Vector3 newPosition = transform.position + (transform.right * speed * Time.deltaTime);
        myRB.MovePosition(newPosition);

        //Check if the bullet should be destroyed
        if (timeDestroy >= startTimeDestroy)
        {
            Destroy(gameObject);
        }
        else if(!manager.isPaused)
        {
            timeDestroy += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Obstacle"))
        {
            if (other.GetComponent<Draggable>()) {
                other.GetComponent<Draggable>().durability -= dmg;
            } else if (other.GetComponent<SolidWallTileSet>() && other.GetComponent<SolidWallTileSet>().penetrable && penetrate) {
                return;
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