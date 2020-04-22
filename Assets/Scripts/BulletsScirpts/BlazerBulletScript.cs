using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlazerBulletScript : MonoBehaviour
{
    Rigidbody2D bulletRB;
    GameManager manager;
    Vector3 dir;
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
    }

    private void Update()
    {
        MoveTowards(dir);
        Destroy(this, lifetime);
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
        Debug.Log(collision.gameObject.name);
        if (collision.transform.tag.Equals("Player"))
        {
            Debug.Log("hit player");
            Destroy(this.gameObject);
        }
        if (collision.gameObject.name.Equals("Walls"))
        {
            Destroy(this.gameObject);
        }

    }
}
