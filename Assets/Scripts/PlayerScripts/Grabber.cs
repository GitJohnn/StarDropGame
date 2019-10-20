using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Grabber : MonoBehaviour
{
    public bool canShoot = true;
    public float timeToShoot = 0.3f;
    public float raycastSize = 1f;
    public float shootPower = 4f;
    public float maxGrip = 3f;
    public float waitTime = .25f;

    float grip;
    bool hldObj = false;
    bool soreHands = false;
    bool canGrab = true;
    float originalDurability;
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
        if (d != null)
        {
            //only if object exist.
            GrabObject();
            LetGoObject();
        }
        //update when not grabbing object
        UpdateGrip();
        //SoreHands Effect.
        SoreHands();
    }

    public void GrabObject()
    {

        if (d.tag.Equals("Enemy"))
        {
            if (grabber.IsTouching(d.GetComponent<CapsuleCollider2D>()))
            {
                if (Input.GetMouseButtonDown(1) && d.draggable && canGrab)
                {
                    d.GetComponent<EnemyAI>().useDefaultMovement = false;
                    //we set it as a child of the stem
                    d.transform.SetParent(this.transform.parent);
                    d.transform.position = this.transform.position;
                    Debug.Log(d.transform.name + " is Grabbed");
                    d.transform.rotation = Quaternion.identity;
                    d.IsHeld = true;
                    hldObj = true;
                    canShoot = false;
                    canGrab = false;
                    path.Scan();
                    timeDelay = 0;
                }
            }
        }
        else if (d.tag.Equals("Obstacle"))
        {
            if (grabber.IsTouching(d.GetComponent<BoxCollider2D>()))
            {
                if (Input.GetMouseButtonDown(1) && d.draggable && canGrab)
                {
                    //we set it as a child of the stem
                    d.transform.SetParent(this.transform.parent);
                    d.transform.position = this.transform.position;
                    Debug.Log(d.transform.name + " is Grabbed");
                    d.gameObject.layer = 0;
                    d.transform.rotation = this.transform.rotation;
                    d.IsHeld = true;
                    originalDurability = d.durability;
                    hldObj = true;
                    canShoot = false;
                    canGrab = false;
                    path.Scan();
                    timeDelay = 0;
                }
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
                StartCoroutine(canActAgain(waitTime));
            }
        }
        // check if the object is destroyed while being held special of Obstacles
        if (d.tag.Equals("Obstacle") && hldObj)
        {
            if(d.durability < originalDurability)
            {
                originalDurability = d.durability;
                grip -= maxGrip / 4f;
            }
            if (d.durability <= 0)
            {
                d = null;
                hldObj = false;
                canShoot = true;
                StartCoroutine(canActAgain(waitTime));
            }
        }
        //to keep the object at the grabbers location
        if (hldObj && d.transform.localPosition != transform.localPosition + addDirection)
        {      
            d.transform.localPosition = transform.localPosition + addDirection;
        }
        //drop gripTimer if grabbing enemy
        if (hldObj && d.tag.Equals("Enemy"))
        {
            grip -= Time.deltaTime;
        }
    }

    void UpdateGrip()
    {
        if (grip < maxGrip && !hldObj)
        {
            grip += Time.deltaTime;
        }
        else if (grip > maxGrip)
        {
            grip = maxGrip;
        }
        //here we want to always make sure we can't shoot if we're holding something
        if (hldObj)
        {
            canShoot = false;
        }
        else
        {
            canShoot = true;
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
            d.IsHeld = false;
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
            Debug.Log(d.name + " dropped");
            if(d.tag.Equals("Obstacle"))
            {
                d.gameObject.layer = 8;
                path.Scan();
            }
            //we detach the object
            d.transform.parent = null;
            d.transform.rotation = Quaternion.identity;
            d.IsHeld = false;
            StartCoroutine(canActAgain(waitTime));
            hldObj = false;
            d = null;
        }
        else if (Input.GetMouseButtonDown(0) && hldObj)
        {
            if (d.tag.Equals("Obstacle"))
            {
                d.gameObject.layer = 8;
            }
            d.transform.parent = null;
            d.transform.rotation = Quaternion.identity;
            d.IsHeld = false;
            d.ShootObj(true, shootDir.transform.right, path);
            StartCoroutine(canActAgain(waitTime));
            hldObj = false;
            d = null;
        }
        else
        {
            timeDelay += Time.deltaTime;
        }
    }

    public bool HldObj
    {
        get
        {
            return hldObj;
        }
    }

    public float Grip
    {
        get { return grip; }
        set { grip = value; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Draggable>() && d==null && !hldObj && canGrab)
        {
            d = collision.GetComponent<Draggable>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponent<Draggable>() && d==null && !hldObj && canGrab)
        {
            d = collision.GetComponent<Draggable>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (d && !hldObj)
        {
            canShoot = true;
            d = null;
        }
    }

    IEnumerator canActAgain(float time)
    {
        yield return new WaitForSeconds(time);
        canShoot = true;
        canGrab = true;
    }

}
