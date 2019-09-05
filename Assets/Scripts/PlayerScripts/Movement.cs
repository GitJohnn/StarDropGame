using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    Rigidbody2D myRB;
    GameObject player;
    Vector3 moveVelocity;

    public float airTime;
    public float speed;
    public float maxStamina = 100f;
    public float maxHealth = 100f;
    float health;
    float stamina;
    float knockTime = .2f;
    float dashTime = .4f;
    float dashSpeed = 13f;
    Vector3 dashDir;
    public bool isJumping = false;
    bool knockedOrDash;
    bool GameOver;

    // Start is called before the first frame update
    void Awake()
    {
        stamina = maxStamina;
        health = maxHealth;
        GameOver = false;
        myRB = GetComponent<Rigidbody2D>();
        player = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!knockedOrDash & !isJumping)
        {
            myRB.velocity = moveVelocity;
            if (Input.GetKeyDown(KeyCode.Q))
            {
                isJumping = true;
                Jump();
            }
            GetInput();
            Dash(dashDir);
        }

        if(stamina < maxStamina)
        {
            stamina += Time.deltaTime;
        }
        else if(stamina > maxStamina)
        {
            stamina = maxStamina;
        }
    }

    void GetInput()
    {
        Vector3 moveTowards = new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical") ,0f);
        dashDir = moveTowards;
        moveVelocity = moveTowards.normalized * speed;
    }

    void Jump()
    {
        //set velocity equal to zero.
        myRB.velocity = Vector2.zero;
        StartCoroutine(AirTime(airTime));
    }

    void Dash(Vector3 dir)
    {
        float costOfStamina = 7.5f;
        //dash in mouse direction in case there is no movement
        if(dir == Vector3.zero && Input.GetKeyDown(KeyCode.Space) && (stamina >= costOfStamina))
        {
            stamina -= costOfStamina;
            dir = GameObject.Find("stem").GetComponent<Transform>().transform.right;
        }

        if (Input.GetKeyDown(KeyCode.Space) && (stamina >= costOfStamina))
        {
            stamina -= costOfStamina;
            knockedOrDash = true;
            myRB.velocity = Vector3.zero;
            myRB.AddForce(dir * dashSpeed, ForceMode2D.Impulse);
            StartCoroutine(KnockBackAndDash(dashTime));
        }
    }

    public void Damage(float dmg)
    {
        if(health > 0)
        {
            health -= dmg;
        }
        else if (health == 0)
        {
            GameOver = true;
        }
    }

    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    public float Stamina
    {
        get { return stamina; }
        set { stamina = value; }
    }

    public void takeKnockBack(Vector3 position, float knockBackForce)
    {
        knockedOrDash = true;
        myRB.velocity = Vector3.zero;
        Vector3 direction = Vector3.Normalize(transform.position - position);
        myRB.AddForce(direction * knockBackForce, ForceMode2D.Impulse);
        StartCoroutine(KnockBackAndDash(knockTime));
    }

    IEnumerator AirTime(float time)
    {
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = .25f;
        GetComponent<SpriteRenderer>().color = tmp;
        yield return new WaitForSeconds(time);
        isJumping = false;
        tmp.a = 1f;
        GetComponent<SpriteRenderer>().color = tmp;
    }

    IEnumerator KnockBackAndDash(float time)
    {
        yield return new WaitForSeconds(time);
        knockedOrDash = false;
    }

}
