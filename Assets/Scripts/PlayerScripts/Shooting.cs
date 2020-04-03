using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform shotPos;
    float shootrate = 0.1f;
    float currentShootrate = 0;
    GameManager manager;
    //short for grabber
    Grabber grabObj;

    //Control variables
    PlayerControls controls;
    Vector2 aim;

    private void Awake()
    {
        manager = GameObject.Find("Manager").GetComponent<GameManager>();
        grabObj = GameObject.FindGameObjectWithTag("Grabber").GetComponent<Grabber>();

        controls = new PlayerControls();
        controls.Gameplay.Shoot.performed += ctx => Shoot();

        controls.Gameplay.Aim_RS.performed += ctx => aim = ctx.ReadValue<Vector2>();
        controls.Gameplay.Aim_RS.canceled += ctx => aim = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!manager.IsPaused && !manager.IsGameOver)
        {
            //if controller is plugged in then don't rotate towards the mouse.
            RotateArm();
        }
        //update currentshootrate
        currentShootrate += Time.deltaTime;
    }

    void RotateArm()
    {
        Vector3 difference;
        if (!aim.Equals(Vector2.zero))
            difference = new Vector3(aim.x, aim.y, 0);
        else
            difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f , rotZ);
    }

    void Shoot()
    {
        if (grabObj.canShoot && !manager.IsPaused && !manager.IsGameOver && (currentShootrate >= shootrate))
        {
            Instantiate(bullet,shotPos.position,transform.rotation);
            currentShootrate = 0;
        }
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

}
