using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemScript : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletSpawn;

    public float dmg;
    public float stompDmg;
    public float stompKnockBack;
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
        Instantiate(bullet, bulletSpawn.transform.position, transform.rotation);
        timeAttack = 0;
        isAttacking = false;
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
            if (ray.collider.tag.Equals("Player"))
            {
                ray.collider.GetComponent<Movement>().Damage(stompDmg);
                ray.collider.GetComponent<Movement>().takeKnockBack(transform.position, stompKnockBack);
                Debug.Log("Player was stomped");
            }
            if (ray.collider.tag.Equals("Enemy") && ray.collider != gameObject.GetComponent<CircleCollider2D>())
            {
                ray.collider.GetComponent<EnemyAI>().Damage(stompDmg);
                ray.collider.GetComponent<EnemyAI>().takeKnockBack(transform.position, stompKnockBack);
                Debug.Log("Enemy was stomped");
            }
        }
        yield return new WaitForSeconds(time);
        Debug.Log("stomp stop");
        timeAttack = 0;
        isAttacking = false;
        golemRB.constraints = RigidbodyConstraints2D.None;
    }
}