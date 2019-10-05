﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    Rigidbody2D myRB;
    GameObject player;
    Vector3 moveVelocity;

    public float airTime;
    public float SPEED;
    public float maxStamina = 100f;
    public float maxHealth = 100f;
    public float staminaCostofDash = 7.5f;
    public float baseSpeed;
    public float modSpeed;
    float currentSpeed;
    float health;
    float stamina;
    float knockTime = .2f;
    float dashTime = .2f;
    float dashSpeed = 13f;
    Vector3 dashDir;
    public bool isJumping = false;
    bool knockedOrDash;
    bool GameOver;

    // Start is called before the first frame update
    void Awake()
    {
        currentSpeed = modSpeed + baseSpeed;
        SPEED = currentSpeed;
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
        UpdateStamina();
    }

    void UpdateStamina()
    {
        if (stamina < maxStamina)
        {
            stamina += Time.deltaTime * 1f;
        }
        else if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
    }

    void GetInput()
    {
        Vector3 moveTowards = new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical") ,0f);
        dashDir = moveTowards;
        moveVelocity = moveTowards.normalized * currentSpeed;
    }

    void Jump()
    {
        //set velocity equal to zero.
        myRB.constraints = RigidbodyConstraints2D.FreezeAll;
        StartCoroutine(AirTime(airTime));
    }

    void Dash(Vector3 dir)
    {
        //dash in mouse direction in case there is no movement
        if(dir == Vector3.zero && Input.GetKeyDown(KeyCode.Space) && (stamina >= staminaCostofDash))
        {
            stamina -= staminaCostofDash;
            dir = GameObject.Find("stem").GetComponent<Transform>().transform.right;
            knockedOrDash = true;
            myRB.velocity += (Vector2)dir.normalized * dashSpeed;
            StartCoroutine(KnockBackAndDash(dashTime));
        }

        if (Input.GetKeyDown(KeyCode.Space) && (stamina >= staminaCostofDash))
        {
            stamina -= staminaCostofDash;
            knockedOrDash = true;
            myRB.velocity += (Vector2)dir.normalized * dashSpeed;
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

    public float CurrentSpeed
    {
        get { return currentSpeed; }
        set { currentSpeed = value; }
    }

    public void takeKnockBack(Vector3 position, float knockBackForce)
    {
        knockedOrDash = true;
        myRB.velocity = Vector3.zero;
        Vector3 direction = Vector3.Normalize(transform.position - position);
        myRB.velocity += (Vector2)direction.normalized * knockBackForce;
        StartCoroutine(KnockBackAndDash(knockTime));
    }

    IEnumerator AirTime(float time)
    {
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = .25f;
        GetComponent<SpriteRenderer>().color = tmp;
        yield return new WaitForSeconds(time);
        myRB.constraints = RigidbodyConstraints2D.FreezeRotation;
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
