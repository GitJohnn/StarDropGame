using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Grabber : MonoBehaviour
{
    public bool hldObj = false;
    public bool canShoot = true;
    public float timeToShoot = 0.3f;
    public float raycastSize = 1f;
    public float shootPower = 4f;
    public float maxWeight = 100f;

    float startTimeDelay = 0.5f;
    float timeDelay = 0f;
    private BoxCollider2D grabber;
    private Draggable d = null;
    Shooting shootDir;
    AstarPath path;

    private void Start()
    {
        shootDir = GetComponentInParent<Shooting>();
        grabber = GetComponent<BoxCollider2D>();
        path = GameObject.FindGameObjectWithTag("A*").GetComponent<AstarPath>();
    }

    private void Update()
    {
        GrabObject();
        LetGoObject();
    }

    public void GrabObject()
    {
        
        if (d)
        {
            //Debug.Log(d.transform.parent);
            //Debug.DrawRay(transform.position, Vector3.right);
            //Debug.Log(hit.Length);

            //Debug.Log(grabber.IsTouching(d.GetComponent<BoxCollider2D>()));
            if (grabber.IsTouching(d.GetComponent<BoxCollider2D>()))
            {
                canShoot = false;
                if (Input.GetMouseButtonDown(0) && d.draggable && (maxWeight >= d.weight))
                {
                    //we set it as a child of the stem
                    d.transform.SetParent(this.transform.parent);
                    d.transform.position = this.transform.position;
                    Debug.Log(d.transform.name + " is Grabbed");
                    d.gameObject.layer = 0;
                    d.transform.rotation = Quaternion.identity;
                    hldObj = true;
                    canShoot = false;
                    path.Scan();
                    //Debug.Log("Grabbed");
                    timeDelay = 0;
                }
            }
            if (hldObj && d.transform.parent != this.transform.parent)
            {
                d = this.transform.parent.GetComponentInChildren<Draggable>();
                Debug.Log(d.transform.name);
            }
        }
    }

    public void LetGoObject()
    {
        if (Input.GetMouseButtonDown(0) && hldObj && (timeDelay >= startTimeDelay))
        {
            //we detach the object
            d.transform.parent = null;
            d.transform.rotation = Quaternion.identity;
            d.gameObject.layer = 8;
            path.Scan();
            hldObj = false;
            canShoot = true;
            //Debug.Log(d.transform.parent);
            //Debug.Log("Let Go");
            d = null;
        }
        else if (Input.GetMouseButtonDown(1) && hldObj)
        {
            d.transform.parent = null;
            d.transform.rotation = Quaternion.identity;
            d.gameObject.layer = 8;
            hldObj = false;
            canShoot = true;
            //Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
            d.ShootObject(true, shootDir.transform.right , path);
            d = null;
        }
        else
        {
            timeDelay += Time.deltaTime;
            //Debug.Log(timeDelay);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Draggable>())
        {
            d = collision.GetComponent<Draggable>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(collision.transform.name);
        canShoot = true;
    }
}
