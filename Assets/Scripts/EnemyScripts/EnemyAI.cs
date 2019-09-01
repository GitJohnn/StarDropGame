﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyAI : MonoBehaviour {

    GameObject player;
    Seeker seeker;
    Rigidbody2D rb;

    [SerializeField] float health = 100f;

    [SerializeField] float speed;
    [SerializeField] float nextWaypointDistance = .1f;

    [SerializeField] Vector2 target;
    Path path;
    int currentWaypoint = 0;
    bool atEndOfPath = false;

    [SerializeField] Vector3 homePoint;
    [SerializeField] float chaseRadius;
    [SerializeField] float attackRadius;
    [SerializeField] float stopRadius;
    float distanceToPlayer;

    [SerializeField] GameObject bullet;
    [SerializeField] float timeBetweenShots = 1f;
    float timeSinceLastShot = 1f;

    //To Do list
    //create home point radius to roam
    //wounder around home point
    //add knockback to being hit
    //circle player when attacking
    //add multiple enemy AI options
    //health component
    //organize variable


    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("Player");
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        target = homePoint;
        InvokeRepeating("UpdatePath", 0f, .05f); //Updates Path every 0.05 seconds
        UpdatePath();
    }

    private void UpdatePath() {
        if (seeker.IsDone()) {
            seeker.StartPath(rb.position, target, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p) {
        if (!p.error) { // if there are no errors when creating a path
            path = p;   // set the generated path to the path variable
            currentWaypoint = 0; // and reset the current waypoint
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (attackRadius >= distanceToPlayer) {
            Attack();
        }
        UpdateTarget();
        FollowPath();
        timeSinceLastShot += Time.deltaTime;
    }

    //Moves Along Path
    private void FollowPath() {
        if (path == null) //if the path doesn't exist, then exit update
            return;

        atEndOfPath = currentWaypoint >= path.vectorPath.Count;
        if (atEndOfPath) {
            return;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance) {
            currentWaypoint++;
        }
    }

    private void UpdateTarget() { //changes target depending on condition "AI"
        if (chaseRadius >= distanceToPlayer) {
            target = player.transform.position;
            if (stopRadius >= distanceToPlayer) {
                target = transform.position;
            }
        } else {
            target = homePoint;
        };
    }

    private void OnDrawGizmos() { //Draws Gizmos delete later When not needed
        Gizmos.DrawWireSphere(homePoint, .1f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, stopRadius);
        Gizmos.color = new Color(.5f, 0f, 0f);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    private void Attack() { //Fires bullets
        if (timeSinceLastShot >= timeBetweenShots) {
            Instantiate(bullet, transform.position, Quaternion.Euler(0f, 0f, Mathf.Rad2Deg * Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x))));
            timeSinceLastShot = 0;
        }
    }

    public void Damage(float dmg)
    {
        if(health > 0f)
        {
            health -= dmg;
        }
        else if(health == 0f)
        {
            //Debug.Log("Enemy health is " + health + ", Destroy!");
            Destroy(gameObject);
        }
    }

    public void takeKnockBack(Vector3 position, float knockBackForce) {
        Vector3 direction = Vector3.Normalize(transform.position - position);
        rb.AddForce(direction*knockBackForce, ForceMode2D.Impulse);
    }
}
