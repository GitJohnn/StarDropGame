using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemScript : MonoBehaviour
{
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
    public LayerMask stompVictims;
    bool isAttacking = false;
    float timeAttack;
    float distance;

    public void Attack(GameObject player, float attackDis, float stopDis, Rigidbody2D golemRB)
    {
        distance = DistanceToPlayer(player);
        if (timeAttack >= startTimeAttack && (distance <= attackDis) && (distance >= stopDis) && !isAttacking)
        {
            //We make a Boulder attack;
            StartCoroutine(BolderThrowDelay(bolderAttackDelay, golemRB));
        }
        else if(timeAttack >= startTimeAttack && (distance <= attackDis) && (distance <= stopDis) && !isAttacking)
        {
            //make the Stomp attack
            StartCoroutine(StompingDelay(stompAttackDelay,stopDis,golemRB));
            Debug.Log("stomp begin");
        }
        else if(!isAttacking)
        {
            timeAttack += Time.deltaTime;
        }
    }

    float DistanceToPlayer(GameObject target)
    {
        float temp = Vector3.Distance(transform.position, target.transform.position);
        return temp;
    }

    IEnumerator BolderThrowDelay(float time, Rigidbody2D golemRB)
    {
        isAttacking = true;
        golemRB.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(time);
        isAttacking = false;
        Instantiate(bullet, bulletSpawn.transform.position, transform.rotation);
        timeAttack = 0;
        golemRB.constraints = RigidbodyConstraints2D.None;
    }

    IEnumerator StompingDelay(float time,float stompRadius,Rigidbody2D golemRB)
    {
        isAttacking = true;
        golemRB.constraints = RigidbodyConstraints2D.FreezePosition;
        RaycastHit2D[] sphereInfo = Physics2D.CircleCastAll(transform.position, stompRadius, -Vector2.up, stompRadius * 2, stompVictims);
        //check if we hitplayer
        Debug.Log("Stomp hit " + sphereInfo.Length + " enemies");
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