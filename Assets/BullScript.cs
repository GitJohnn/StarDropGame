using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullScript : MonoBehaviour {
    GameObject player;
    EnemyAI AI;
    Rigidbody2D rb;

    public float chargeTime;
    public float chargingSpeed = 20f;
    public float dmg = 20;
    float timeElapsed;
    float stunTime = 0;
    float currentHealth = 0;
    bool isAttacking = false;
    bool startAttacking = false;

    Vector3 vectorDifference;
    Vector3 direction;

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
    }

    public void Attack() {
        if (stunTime > 0) {
            stunTime -= Time.deltaTime;
        } else {
            if (AI.isInAttackRaduis()) {
                timeElapsed += Time.deltaTime;
            } else {
                timeElapsed = 0;
            }

            if (timeElapsed >= chargeTime && !isAttacking) {
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
        timeElapsed = 0f;
        rb.velocity = new Vector3();
        if (collision.gameObject == player) {
            player.GetComponent<Movement>().Damage(dmg);
        } else {
            stunTime = 3;
        }
        
    }
    

}
