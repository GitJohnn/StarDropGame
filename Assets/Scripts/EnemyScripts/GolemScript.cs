using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemScript : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletSpawn;

    public float dmg;
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
        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, transform.up, stompRadius, stompVictims);
        if (hitinfo.collider != null)
        {
            Debug.Log("stomp hit smthng");
            Debug.Log(hitinfo.transform.name);
        }
        yield return new WaitForSeconds(time);
        Debug.Log("stomp stop");
        timeAttack = 0;
        isAttacking = false;
        golemRB.constraints = RigidbodyConstraints2D.None;
    }
}