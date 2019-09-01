using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform shotPos;
    GameManager isPaused;
    //short for grabber
    Grabber grabObj;

    private void Awake()
    {
        isPaused = GameObject.Find("Manager").GetComponent<GameManager>();
        grabObj = GameObject.FindGameObjectWithTag("Grabber").GetComponent<Grabber>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused.PausePanel.activeInHierarchy)
        {
            RotateArm();
            Shoot();
        }
    }

    void RotateArm()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f , rotZ);
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && grabObj.canShoot)
        {
            Instantiate(bullet,shotPos.position,transform.rotation);
        }
    }

}
