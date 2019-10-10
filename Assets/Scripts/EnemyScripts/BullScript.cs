using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BullScript : MonoBehaviour {
    GameObject player;
    EnemyAI AI;
    Rigidbody2D rb;

    public float chargeTime;
    public float waitTime;
    public float chargingSpeed = 20f;
    public float dmg = 20;
    float timeElapsedInRadius;
    float timeElapsedNotInRadius;
    float stunTime = 0;
    float currentHealth = 0;
    bool isAttacking = false;
    bool startAttacking = false;
    bool returnHome = false;

    Vector3 vectorDifference;
    Vector3 direction;

    //For Moving
    Path path;
    int currentWaypoint = 0;
    bool atEndOfPath = false;

    // Start is called before the first frame update
    void Start() {
        AI = GetComponent<EnemyAI>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if (player == null) {
            player = AI.player;
        }

        SetRigidbodyRestraints(!(isAttacking ^ returnHome));

        if (!AI.isInAttackRaduis()) {
            timeElapsedInRadius = 0;
            timeElapsedNotInRadius += Time.deltaTime;
        }
        returnHome = timeElapsedNotInRadius >= waitTime;

        if (returnHome) {
            AI.FollowPath();
        }
        Debug.Log(returnHome);
        Debug.Log(isAttacking);

    }

    public void Attack() {
        if (stunTime > 0) {
            stunTime -= Time.deltaTime;
        } else {
            if (AI.isInAttackRaduis()) {
                timeElapsedInRadius += Time.deltaTime;
                timeElapsedNotInRadius = 0;
            }

            if (timeElapsedInRadius >= chargeTime && !isAttacking) {
                startAttacking = true;
            }

            if (startAttacking) {
                startAttacking = false;
                vectorDifference = player.transform.position - transform.position;
                direction = vectorDifference / Vector3.Magnitude(vectorDifference);
                isAttacking = true;
                currentHealth = AI.Health;
            }

            if (isAttacking) {
                ChargingForward();
                if (currentHealth - 20f >= AI.Health) {
                    isAttacking = false;
                    stunTime = 2f;
                }
            }
        }
    }

    void ChargingForward() {
        rb.AddForce(new Vector2(direction.x, direction.y) * chargingSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        isAttacking = false;
        timeElapsedInRadius = 0f;
        rb.velocity = new Vector3();
        if (collision.gameObject == player) {
            player.GetComponent<Movement>().Damage(dmg);
        } else {
            stunTime = 3;
        }
        
    }

    //turns on and off rigidbody
    void SetRigidbodyRestraints(bool b) {
        if (b) {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        } else {
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }
    

}
