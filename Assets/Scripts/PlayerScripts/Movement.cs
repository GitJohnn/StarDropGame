using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Rigidbody2D myRB;
    GameObject player;
    Vector3 moveVelocity;
    Vector3 moveTowards;

    public float airTime;
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

    //Control variables
    PlayerControls controls;
    Vector2 move;

    // Start is called before the first frame update
    void Awake()
    {
        stamina = maxStamina;
        health = maxHealth;
        myRB = GetComponent<Rigidbody2D>();
        player = this.gameObject;

        controls = new PlayerControls();
        controls.Gameplay.Move_LS.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move_LS.canceled += ctx => move = Vector2.zero;
        controls.Gameplay.Jump.performed += ctx => Jump();
        controls.Gameplay.Dash.performed += ctx => Dash(dashDir);
    }

    // Update is called once per frame
    void Update()
    {
        if (!knockedOrDash & !isJumping)
        {
            myRB.velocity = moveVelocity;
            //if (Input.GetKeyDown(KeyCode.Q))
            //{
            //    isJumping = true;
            //    Jump();
            //}
            GetInput();
            //Dash(dashDir);
        }
        UpdateStamina();
        //UpdateSpeed
        currentSpeed = modSpeed + baseSpeed;
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
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
        moveTowards = new Vector3(move.x, move.y ,0f);
        dashDir = moveTowards;
        moveVelocity = moveTowards.normalized * currentSpeed;
    }

    void Jump()
    {
        if (!knockedOrDash & !isJumping)
        {
            isJumping = true;
            //set velocity equal to zero.
            myRB.constraints = RigidbodyConstraints2D.FreezeAll;
            StartCoroutine(AirTime(airTime));
        }
    }

    void Dash(Vector2 dir)
    {
        if (!knockedOrDash & !isJumping)
        {
            //dash in mouse direction in case there is no movement
            if (dir == Vector2.zero && (stamina >= staminaCostofDash))
            {
                stamina -= staminaCostofDash;
                dir = GameObject.Find("stem").GetComponent<Transform>().transform.right;
                knockedOrDash = true;
                myRB.velocity += dir.normalized * dashSpeed;
                StartCoroutine(KnockBackAndDash(dashTime));
            }
            else if (stamina >= staminaCostofDash)
            {
                stamina -= staminaCostofDash;
                knockedOrDash = true;
                myRB.velocity += dir.normalized * dashSpeed;
                StartCoroutine(KnockBackAndDash(dashTime));
            }
        }
    }

    public void Damage(float dmg)
    {
        if(health > 0f)
        {
            health -= dmg;
        }
        else if (health <= 0f && GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().IsGameOver)
        {
            //We need to stop the player from moving
            this.enabled = false;
        }
    }

    public Vector3 MoveDirection
    {
        get { return moveTowards.normalized; }
    }

    public float Health
    {
        get { return health; }
        set
        {
            if(value + health > maxHealth)
            {
                health = maxHealth;
            }
            else
            {
                health += value;
            }
        }
    }

    public float Stamina
    {
        get { return stamina; }
        set { stamina = value; }
    }

    public float CurrentSpeed
    {
        get { return currentSpeed; }
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
