﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Draggable : MonoBehaviour
{
    public bool draggable = true;
    public float speed = 10f;
    public float startTimeDistance = .5f;
    public float weight;
    public float durability;
    public float damage = 50f;

    Rigidbody2D myRB;
    Vector2 dir;
    AstarPath path;
    bool act = false;
    float TimeDistance = 0f;

    private void Awake()
    {
        path = GameObject.FindGameObjectWithTag("A*").GetComponent<AstarPath>();
        myRB = GetComponent<Rigidbody2D>();
        if (transform.tag.Equals("Obstacle"))
        {
            myRB.mass = weight;
        }
    }

    private void Update()
    {
        if (act)
        {
            ShootObj(act, dir, path);
        }
        if (transform.tag.Equals("Obstacle"))
        {
            destroyObj();
        }
    }

    void destroyObj()
    {
        if(durability <= 0)
        {
            Destroy(gameObject);
            path.Scan();
        }
    }

    void PushObj(Collision2D actObj)
    {
        Vector3 pushDir = actObj.transform.position - transform.position;
        myRB.velocity = Vector3.zero;
        myRB.AddForce(pushDir.normalized * 5f, ForceMode2D.Impulse);
    }

    public void ShootObj(bool travel, Vector2 direction, AstarPath scan)
    {
        act = travel;
        dir = direction;
        path = scan;
        if (TimeDistance <= startTimeDistance)
        {
            this.transform.Translate(direction.normalized * speed * Time.deltaTime);
        }
        else
        {
            EnemyAI enemy = GetComponent<EnemyAI>();
            act = false;
            TimeDistance = 0;
            if (transform.tag.Equals("Obstacle"))
            {
                path.Scan();
            }
            else if (enemy)
            {
                enemy.canAttack = true;
            }
        }
        TimeDistance += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Damage enemy is the object is moving
        if (collision.transform.tag.Equals("Enemy") && act)
        {
            collision.transform.GetComponent<EnemyAI>().Damage(damage);
        }
        // When enemy is thrown take damage when collide with obstacle
        if(collision.transform.tag.Equals("Obstacle") && act && transform.tag.Equals("Enemy"))
        {
            GetComponent<EnemyAI>().Damage(damage);
        }
        //push crates.
        if (collision.transform.tag.Equals("Player") && transform.tag.Equals("Obstacle"))
        {
            PushObj(collision);
        }
    }

}
