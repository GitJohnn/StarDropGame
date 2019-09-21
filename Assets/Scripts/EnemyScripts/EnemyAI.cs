using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyAI : MonoBehaviour {

    public GameObject player;
    Seeker seeker;
    Rigidbody2D rb;
    GameManager manager;

    Draggable isdraggable;
    public bool useDefaultMovement = true;

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

    //attacking scripts
    SlimeScript slime = null;
    GolemScript golem = null;
    BullScript bull = null;

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
        if (attackRadius >= distanceToPlayer && !manager.isPaused) {
            
            //rotates enemy to look at player
            LookAtPlayer();

            //set the enemyAIto use the script given.
            if (GetComponent<SlimeScript>()) {
                slime = GetComponent<SlimeScript>(); // PREFORMANCE: write this in start function to save memory/time
                slime.Attack(player);
            } else if (GetComponent<GolemScript>()) {
                golem = GetComponent<GolemScript>();
                golem.Attack(player, attackRadius, stopRadius, rb);
            } else if (GetComponent<BullScript>()) {
                bull = GetComponent<BullScript>();
                bull.Attack();
            }
        }

        if (useDefaultMovement)
        {
            UpdateTarget();
            FollowPath();
        }

        CheckIfDeath();
    }

    //Look Towards player
    void LookAtPlayer()
    {
        Vector3 difference = player.transform.position - transform.position;
        float rotz = Mathf.Atan2(difference.y,difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotz);
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
        //rb.velocity = force; //this would be better for movement, but doesn't work.

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

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(homePoint, .1f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, stopRadius);
        Gizmos.color = new Color(.5f, 0f, 0f);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
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

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
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

    public Vector3 getHomePoint() {
        return homePoint;
    }

    public bool isInAttackRaduis() {
        return (attackRadius >= distanceToPlayer);
    }

    public bool isInChaseRaduis() {
        return (attackRadius >= distanceToPlayer);
    }

    public bool isInStopRaduis() {
        return (attackRadius >= distanceToPlayer);
    }
}
