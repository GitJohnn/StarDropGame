using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyAI : MonoBehaviour {

    GameObject player;
    Seeker seeker;
    Rigidbody2D rb;
    GameManager manager;

    Draggable isdraggable;
    public bool canAttack = true;

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
    //knock should stop AI so that it seems less floaty
    //wounder around home point
    //circle player when attacking
    //add multiple enemy AI options
    //organize variable


    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("Player");
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        isdraggable = GetComponent<Draggable>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();

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
        if (attackRadius >= distanceToPlayer && canAttack && !manager.isPaused) {
            Attack();
        }

        if (canAttack)
        {
            UpdateTarget();
            FollowPath();
            timeSinceLastShot += Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        CheckIfDeath();
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
            //startMoving();
            if (stopRadius >= distanceToPlayer) {
                target = transform.position;
                //stopMoving();
            }
        } else {
            target = homePoint;
            //startMoving();
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
            Instantiate(bullet, transform.position, Quaternion.Euler(0f, 0f, Mathf.Rad2Deg * Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x))),gameObject.transform);
            timeSinceLastShot = 0;
            Debug.Log("Enemy shoot bullet");
        }
    }

    void CheckIfDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Damage(float dmg)
    {
        if(health > 0f)
        {
            health -= dmg;
        }
    }

    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    public void takeKnockBack(Vector3 position, float knockBackForce) {
        Vector3 direction = Vector3.Normalize(transform.position - position);
        rb.AddForce(direction*knockBackForce, ForceMode2D.Impulse);
    }

    private void stopMoving() {
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    private void startMoving() {
        rb.constraints = RigidbodyConstraints2D.None;
    }
}
