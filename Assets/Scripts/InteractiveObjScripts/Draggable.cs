using System.Collections;
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

    bool isHeld = false;
    Rigidbody2D myRB;
    Vector2 dir;
    AstarPath path;
    bool act = false;
    float timeDistance = 0f;

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

    public void ShootObj(bool travel, Vector2 direction, AstarPath scan)
    {
        // here we make the object grabbed be thrown.
        act = travel;
        dir = direction;
        path = scan;
        if (timeDistance <= startTimeDistance)
        {
            // if we want to make the object bounce we need to change this.
            this.transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
        else
        {
            act = false;
            timeDistance = 0;
            if (transform.tag.Equals("Obstacle"))
            {
                path.Scan();
            }
            else if (transform.tag.Equals("Enemy"))
            {
                GetComponent<EnemyAI>().useDefaultMovement = true;
            }
        }
        timeDistance += Time.deltaTime;
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
    }

    public bool IsHeld
    {
        get
        {
            return isHeld;
        }

        set
        {
            isHeld = value;
        }
    }

}
