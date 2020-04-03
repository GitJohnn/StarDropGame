using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyAI : MonoBehaviour {

    public Rigidbody2D rb;
    GameManager manager;
    Animator slimeAnim;
    Draggable isdraggable;
    //attacking scripts
    SlimeScript slime = null;
    GolemScript golem = null;
    BullScript bull = null;
    BlazerScript blazer = null;

    [SerializeField] float health = 100f;
    [SerializeField] float speed;
    [SerializeField] float nextWaypointDistance = .1f;
    [SerializeField] Vector2 target;
    [SerializeField] float chaseRadius;
    [SerializeField] float attackRadius;
    [SerializeField] float stopRadius;

    Path path;
    Vector3 homePoint;
    int currentWaypoint = 0;
    bool atEndOfPath = false;
    float distanceToPlayer;
    
    public GameObject player;
    public Seeker seeker;
    public float dmgTimer;
    public bool useDefaultMovement = true;
    public bool moving = false;
    public bool isDmg = false;

    // Start is called before the first frame update
    void Start() {
        homePoint = this.transform.position;
        if (this.gameObject.name.Contains("Slime"))
        {
            slime = GetComponent<SlimeScript>();
        }
        else if (this.gameObject.name.Contains("Golem"))
        {
            golem = GetComponent<GolemScript>();
        }
        else if (this.gameObject.name.Contains("Bull"))
        {
            bull = GetComponent<BullScript>();
        }
        else if (this.gameObject.name.Contains("Blazer"))
        {
            blazer = GetComponent<BlazerScript>();
        }
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
        if (attackRadius >= distanceToPlayer && !manager.IsPaused && !manager.IsGameOver) {
            //set the enemyAI to use the script given.
            if (slime) {
                slime.Attack(player);
            } else if (golem) {
                golem.Attack(player, attackRadius, stopRadius, rb);
            } else if (bull) {
                bull.Attack();
            }
        }
        

        if (useDefaultMovement)
        {
            UpdateTarget();
            FollowPath();
        }

        CheckIfDeath();
        //check if moving
        if(rb.velocity.magnitude > 0f)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
    }

    //Look Towards player
    public Quaternion LookAtPlayer()
    {
        Vector3 difference = player.transform.position - transform.position;
        float rotz = Mathf.Atan2(difference.y,difference.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, rotz);
    }


    //Moves Along Path
    public void FollowPath() {
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

    private void OnDrawGizmosSelected() {
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
        if (health > 0f)
        {
            health -= dmg;
        }
        StartCoroutine(damage());
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
        rb.velocity = direction.normalized * knockBackForce;
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

    IEnumerator damage()
    {
        isDmg = true;
        yield return new WaitForSeconds(dmgTimer);
        isDmg = false;
    }
}
