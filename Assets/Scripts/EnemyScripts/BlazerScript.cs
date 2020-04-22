using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlazerScript : MonoBehaviour
{
    SpriteRenderer sprite;
    Animator blazerAnim;
    Rigidbody2D blazerRB;
    BlazerBulletScript bulletscript;
    EnemyAI enemyAI;
    public GameObject bullet;

    public float stopTimer;
    public float startTimer;
    bool attacking = false;
    float timer;

    // Start is called before the first frame update
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        enemyAI = GetComponent<EnemyAI>();
        blazerAnim = GetComponentInChildren<Animator>();
        timer = startTimer / 2;
        blazerRB = GetComponent<Rigidbody2D>();
        bulletscript = bullet.GetComponent<BlazerBulletScript>();
    }

    private void Update()
    {
        BlazerAnimations();
        if (timer >= startTimer && enemyAI.isInAttackRadius())
        {
            attacking = true;
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime * 1;
        }
    }

    private void BlazerAnimations()
    {
        //flip image
        Vector2 direction = enemyAI.player.transform.position - this.transform.position;
        if (direction.x < -0.1)
        {
            sprite.flipX = true;
        }
        else if (direction.x > 0.1)
        {
            sprite.flipX = false;
        }

        if (enemyAI.moving)
        {
            blazerAnim.SetBool("Moving", true);
        }
        else
        {
            blazerAnim.SetBool("Moving", false);
        }

        if (attacking)
        {
            blazerAnim.SetBool("Attacking", true);
        }
        else
        {
            blazerAnim.SetBool("Attacking", false);
        }
    }

    public void Attack()
    {
        GameObject blazerbullet = Instantiate(bullet, this.transform);
        blazerbullet.GetComponent<BlazerBulletScript>().MoveTowards(GameObject.FindGameObjectWithTag("Player").transform.position - blazerbullet.transform.position);
        attacking = false;
    }

    IEnumerator stopToShoot()
    {
        blazerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(stopTimer);
        blazerRB.constraints = RigidbodyConstraints2D.None;
        blazerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
