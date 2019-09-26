using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    CircleCollider2D thorns;
    Rigidbody2D slimeRB;

    public float dmg;
    public float startTimeAttack;
    public float slimeKnockback;
    public float delay;
    float timeAttack = 0;
    bool isColliding = false;

    // Start is called before the first frame update
    void Start()
    {
        thorns = GetComponent<CircleCollider2D>();
        slimeRB = GetComponent<Rigidbody2D>();
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
        else
        {
            timeAttack += Time.deltaTime;
        }
    }

    IEnumerator AttackDelay(float time)
    {
        slimeRB.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(time);
        slimeRB.constraints = RigidbodyConstraints2D.None;
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
