using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    CircleCollider2D thorns;
    Rigidbody2D slimeRB;
    Grabber playerGrab;
    Animator anim;
    EnemyAI enemyAI;

    public float dmg;
    public float startTimeAttack;
    public float slimeKnockback;
    public float delay;
    bool isAttacking;
    float timeAttack = 0;
    bool isColliding = false;

    // Start is called before the first frame update
    void Start()
    {
        playerGrab = GameObject.FindGameObjectWithTag("Grabber").GetComponent<Grabber>();
        thorns = GetComponent<CircleCollider2D>();
        slimeRB = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        enemyAI = GetComponent<EnemyAI>();
    }

    private void Update()
    {
        anim.SetBool("isDmg", enemyAI.isDmg);
    }

    public void Attack(GameObject player)
    {
        if (timeAttack >= startTimeAttack && isColliding)
        {
            player.GetComponent<Movement>().Damage(dmg);
            player.GetComponent<Movement>().takeKnockBack(transform.position,slimeKnockback);
            StartCoroutine(AttackDelay(delay));
            timeAttack = 0;
        }
        else if(!isAttacking && !playerGrab.HldObj)
        {
            timeAttack += Time.deltaTime;
        }
    }

    IEnumerator AttackDelay(float time)
    {
        slimeRB.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(time);
        slimeRB.constraints = RigidbodyConstraints2D.None;
        slimeRB.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Player") && !collision.transform.GetComponent<Movement>().isJumping)
        {
            timeAttack = startTimeAttack;
            isColliding = true;
            Attack(collision.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Player"))
        {
            isColliding = false;
        }
    }
}
