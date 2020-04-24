using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlazerBulletScript : MonoBehaviour
{
    public GameObject fire;
    [HideInInspector]
    public GameObject OriginParent;

    Rigidbody2D bulletRB;
    GameManager manager;
    Vector3 dir;
    int fireCounter = 0;
    int MaxFire = 15;
    float fireTimer = -0.05f;
    float startFireTimer = 0.03f;
    public float BulletDmg = 30f;
    public float speed = 1.5f;
    public float lifetime = 3f;

    private void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    void SetFire()
    {
        //instantiate fire
        if(fireTimer >= startFireTimer && (fireCounter<=MaxFire))
        {
            fireCounter++;
            GameObject newFire = Instantiate(fire, this.transform.position,Quaternion.identity);
            Destroy(newFire, 0.95f);
            fireTimer = 0;
        }
        else
        {
            fireTimer += 1 * Time.deltaTime;
        }
    }

    private void Update()
    {
        MoveTowards(dir);
        SetFire();
        if(fireCounter == MaxFire)
        {
            Destroy(this.gameObject);
        }
    }

    public void MoveTowards(Vector3 direction)
    {
        dir = direction;
        if (bulletRB && !manager.IsPaused)
        {
            bulletRB.velocity = (speed * dir.normalized);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("Player"))
        {
            collision.GetComponent<Movement>().Damage(BulletDmg);
            collision.GetComponent<Movement>().takeKnockBack(this.transform.position,20f);
            GameObject newFire = Instantiate(fire, this.transform.position, Quaternion.identity);
            Destroy(newFire, 0.95f);
            Destroy(this.gameObject);
        }
        if (collision.transform.tag.Equals("Enemy") && (collision.gameObject != OriginParent))
        {
            collision.GetComponent<EnemyAI>().Damage(BulletDmg, 20, 0.8f,this.transform);
            GameObject newFire = Instantiate(fire, this.transform.position, Quaternion.identity);
            Destroy(newFire, 0.95f);
            Destroy(this.gameObject);
        }
        if (collision.gameObject.name.Equals("Walls"))
        {
            Destroy(this.gameObject);
        }

    }
}
