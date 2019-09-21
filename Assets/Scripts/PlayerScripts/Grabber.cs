using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Grabber : MonoBehaviour
{
    public bool canShoot = true;
    public bool hldObj = false;
    public float timeToShoot = 0.3f;
    public float raycastSize = 1f;
    public float shootPower = 4f;
    public float maxGrip = 3f;
    public float waitTimeToGrab = .25f;

    float grip;
    bool soreHands = false;
    bool canGrab = true;
    float startTimeDelay = 0.1f;
    float timeDelay = 0f;
    private BoxCollider2D grabber;
    private Draggable d = null;
    Shooting shootDir;
    AstarPath path;
    Vector3 addDirection = new Vector3(.35f, 0f, 0f);

    private void Start()
    {
        grip = maxGrip;
        shootDir = GetComponentInParent<Shooting>();
        grabber = GetComponent<BoxCollider2D>();
        path = GameObject.FindGameObjectWithTag("A*").GetComponent<AstarPath>();
    }

    private void Update()
    {
        GrabObject();
        LetGoObject();
        UpdateGrip();
    }

    public void GrabObject()
    {
        if (d)
        {
            if (!d.tag.Equals("Obstacle"))
            {
                if (grabber.IsTouching(d.GetComponent<CircleCollider2D>()))
                {
                    canShoot = false;
                    if (Input.GetMouseButtonDown(1) && d.draggable && canGrab)
                    {
                        d.GetComponent<EnemyAI>().useDefaultMovement = false;
                        //we set it as a child of the stem
                        d.transform.SetParent(this.transform.parent);
                        d.transform.position = this.transform.position;
                        Debug.Log(d.transform.name + " is Grabbed");
                        //d.gameObject.layer = 0;
                        d.transform.rotation = Quaternion.identity;
                        hldObj = true;
                        canShoot = false;
                        canGrab = false;
                        path.Scan();
                        timeDelay = 0;
                    }
                }
            }
            else if (grabber.IsTouching(d.GetComponent<BoxCollider2D>()) && d.tag.Equals("Obstacle"))
            {
                canShoot = false;
                if (Input.GetMouseButtonDown(1) && d.draggable && canGrab)
                {
                    //we set it as a child of the stem
                    d.transform.SetParent(this.transform.parent);
                    d.transform.position = this.transform.position;
                    d.gameObject.layer = 0;
                    d.transform.rotation = Quaternion.identity;
                    hldObj = true;
                    canShoot = false;
                    canGrab = false;
                    path.Scan();
                    timeDelay = 0;
                }
            }
            // make sure we grab correct obj
            if (hldObj && d.transform.parent != this.transform.parent)
            {
                d = this.transform.parent.GetComponentInChildren<Draggable>();
            }
            //check if enemy was destroyed while grabbed
            if (d.tag.Equals("Enemy") && hldObj)
            {
                if (d.GetComponent<EnemyAI>().Health <= 0)
                {
                    d = null;
                    hldObj = false;
                    canShoot = true;
                    StartCoroutine(canGrabAgain(waitTimeToGrab));
                }
            }
            // check if the object is destroyed while being held special of Obstacles
            if (d.tag.Equals("Obstacle") && hldObj)
            {
                if (d.durability <= 0)
                {
                    d = null;
                    hldObj = false;
                    canShoot = true;
                    StartCoroutine(canGrabAgain(waitTimeToGrab));
                }
            }
            //to keep the object at the grabbers location
            if (hldObj && d.transform.localPosition != transform.localPosition + addDirection)
            {      
                d.transform.localPosition = transform.localPosition + addDirection;
            }
            //SoreHands Effect
            SoreHands();
        }
    }

    void UpdateGrip()
    {
        //drop or add to the GripRate
        if (hldObj)
        {
            grip -= Time.deltaTime;
        }
        else if (grip < maxGrip && !hldObj)
        {
            grip += Time.deltaTime;
        }
        else if (grip > maxGrip)
        {
            grip = maxGrip;
        }
    }

    void SoreHands()
    {
        if(hldObj && grip <= 0f)
        {
            if (!d.tag.Equals("Obstacle"))
            {
                d.GetComponent<EnemyAI>().useDefaultMovement = true;
            }
            else if (d.tag.Equals("Obstacle"))
            {
                d.gameObject.layer = 8;
                path.Scan();
            }
            soreHands = true;
            d.transform.parent = null;
            d.transform.rotation = Quaternion.identity;
            hldObj = false;
            canShoot = true;
            d = null;
        }
        if(soreHands && grip >= maxGrip)
        {
            soreHands = false;
            canGrab = true;
        }
    }

    public void LetGoObject()
    {
        if (Input.GetMouseButtonDown(1) && hldObj && (timeDelay >= startTimeDelay))
        {
            //is used for enemies to stop shooting
            if (!d.tag.Equals("Obstacle"))
            {             
                d.GetComponent<EnemyAI>().useDefaultMovement = true;
            }
            else if(d.tag.Equals("Obstacle"))
            {
                path.Scan();
                d.gameObject.layer = 8;
            }
            //we detach the object
            d.transform.parent = null;
            d.transform.rotation = Quaternion.identity;
            hldObj = false;
            canShoot = true;
            StartCoroutine(canGrabAgain(waitTimeToGrab));
            //Debug.Log(d.transform.parent);
            //Debug.Log("Let Go");
            d = null;
        }
        else if (Input.GetMouseButtonDown(0) && hldObj)
        {
            if (d.tag.Equals("Obstacle"))
            {
                d.gameObject.layer = 8;
            }
            //is being dragged variable is used for enemies to stop shooting.
            d.transform.parent = null;
            d.transform.rotation = Quaternion.identity;
            hldObj = false;
            canShoot = true;
            StartCoroutine(canGrabAgain(waitTimeToGrab));
            //Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
            d.ShootObj(true, shootDir.transform.right , path);
            d = null;
        }
        else
        {
            timeDelay += Time.deltaTime;
        }
    }

    public float Grip
    {
        get { return grip; }
        set { grip = value; }
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

    IEnumerator canGrabAgain(float time)
    {
        yield return new WaitForSeconds(time);
        canGrab = true;
    }
}
