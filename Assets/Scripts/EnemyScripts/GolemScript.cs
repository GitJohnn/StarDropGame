using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemScript : MonoBehaviour
{
    public bool canThrowBoulder;
    public bool canStomp;
    public GameObject bullet;
    public GameObject bulletSpawn;
    public GameObject stompGolemBullet;
    public GameObject[] stompBulletSpawn;
    public float dmg;
    public float thornDmg;
    public float stompDmg;
    public float stompKnockback;
    public float thornKnockback;
    public float startTimeAttack;
    public float bolderAttackDelay;
    public float stompAttackDelay;
    EnemyAI enemyAI;
    Grabber playerGrab;
    LayerMask stompVictims = 9;
    bool isAttacking = false;
    float timeAttack;
    float distance;

    private void Awake()
    {
        playerGrab = GameObject.FindGameObjectWithTag("Grabber").GetComponent<Grabber>();
        enemyAI = GetComponent<EnemyAI>();
    }

    private void Update()
    {
        bulletSpawn.transform.parent.rotation = enemyAI.LookAtPlayer();
    }

    public void Attack(GameObject player, float attackDis, float stopDis, Rigidbody2D golemRB)
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if(canThrowBoulder && canStomp)
        {
            //We make a Boulder attack;
            if (canThrowBoulder)
            {
                if (timeAttack >= startTimeAttack && (distance <= attackDis) && (distance >= stopDis) && !isAttacking)
                {
                    StartCoroutine(BolderThrowDelay(bolderAttackDelay, golemRB));
                }
            }
            //make the Stomp attack
            if (canStomp)
            {
                if (timeAttack >= startTimeAttack && (distance <= attackDis) && (distance <= stopDis) && !isAttacking)
                {
                    StartCoroutine(StompingDelay(stompAttackDelay, stopDis, golemRB));
                }
            }
            //add time between attacks
            if (!isAttacking && !playerGrab.HldObj)
            {
                timeAttack += Time.deltaTime;
            }
        }
    }

    IEnumerator BolderThrowDelay(float time, Rigidbody2D golemRB)
    {
        isAttacking = true;
        golemRB.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(time);
        Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        isAttacking = false;
        timeAttack = 0;
        golemRB.constraints = RigidbodyConstraints2D.None;
        golemRB.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    IEnumerator StompingDelay(float time,float stompRadius,Rigidbody2D golemRB)
    {
        isAttacking = true;
        golemRB.constraints = RigidbodyConstraints2D.FreezeAll;
        RaycastHit2D[] sphereInfo = Physics2D.CircleCastAll(transform.position, stompRadius, -Vector2.up, stompRadius * 2, stompVictims);
        foreach (RaycastHit2D ray in sphereInfo)
        {
            if (ray.collider.tag.Equals("Player") && !ray.transform.GetComponent<Movement>().isJumping)
            {
                ray.collider.GetComponent<Movement>().Damage(stompDmg);
                ray.collider.GetComponent<Movement>().takeKnockBack(transform.position, stompKnockback);
            }
            if (ray.collider.tag.Equals("Enemy") && ray.collider != gameObject.GetComponent<CircleCollider2D>())
            {
                ray.collider.GetComponent<EnemyAI>().Damage(stompDmg);
                ray.collider.GetComponent<EnemyAI>().takeKnockBack(transform.position, stompKnockback);
            }
        }
        foreach (GameObject i in stompBulletSpawn)
        {
            Instantiate(stompGolemBullet, i.transform.position, i.transform.rotation);
        }
        yield return new WaitForSeconds(time);
        isAttacking = false;
        timeAttack = 0;
        golemRB.constraints = RigidbodyConstraints2D.None;
        golemRB.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Player") && isAttacking && !collision.transform.GetComponent<Movement>().isJumping)
        {
            collision.transform.GetComponent<Movement>().Damage(thornDmg);
            collision.transform.GetComponent<Movement>().takeKnockBack(transform.position, thornKnockback);
        }
    }

}