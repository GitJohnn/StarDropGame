using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D myRB;
    GameObject player;
    Vector3 moveVelocity;

    public float speed;
    float health = 100;
    float knockTime = .2f;
    bool knocked;
    bool GameOver;

    // Start is called before the first frame update
    void Awake()
    {
        GameOver = false;
        myRB = GetComponent<Rigidbody2D>();
        player = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        if (!knocked)
        {
            myRB.velocity = moveVelocity;
        }
    }

    void GetInput()
    {
        Vector3 moveTowards = new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical") ,0f);
        moveVelocity = moveTowards.normalized * speed;
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
            Debug.Log("Game is Over!");
        }
    }

    public float Health
    {
        get { return health; }
        set { value = health; }
    }

    public void takeKnockBack(Vector3 position, float knockBackForce)
    {
        knocked = true;
        myRB.velocity = Vector3.zero;
        Vector3 direction = Vector3.Normalize(transform.position - position);
        myRB.AddForce(direction * knockBackForce, ForceMode2D.Impulse);
        Debug.Log("Taken Knockback");
        StartCoroutine(KnockBack(knockTime));
    }

    IEnumerator KnockBack(float time)
    {
        yield return new WaitForSeconds(time);
        knocked = false;
    }

}
