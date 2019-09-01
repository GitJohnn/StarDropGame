﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Draggable : MonoBehaviour
{
    public bool draggable = true;
    public float speed = 10f;
    public float startTimeDistance = .5f;
    private bool act = false;
    private Vector2 dir;
    AstarPath path;
    private float TimeDistance = 0f;

    private void Update()
    {
        if (act)
        {
            ShootObject(act, dir, path);
        }
        
    }

    public void ShootObject(bool travel, Vector2 direction, AstarPath scan)
    {
        act = travel;
        dir = direction;
        path = scan;
        if (TimeDistance <= startTimeDistance)
        {
            this.transform.Translate(direction.normalized * speed * Time.deltaTime);
        }
        else
        {
            act = false;
            TimeDistance = 0;
            path.Scan();
        }
        TimeDistance += Time.deltaTime;
    }

}
