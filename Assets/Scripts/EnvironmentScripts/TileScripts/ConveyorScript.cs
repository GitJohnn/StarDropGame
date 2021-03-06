﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorScript : MonoBehaviour
{
    public Vector2 dir;
    public float conveyorSpeed = 5f;
    LayerMask mask;
    List<Transform> overlappingObj = new List<Transform>();


    // Update is called once per frame
    void Update()
    {
        if(overlappingObj.Count != 0)
        {
            for(int i = 0; i < overlappingObj.Count; i++)
            {
                overlappingObj[i].position += (Vector3)dir.normalized * Time.deltaTime * conveyorSpeed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            overlappingObj.Add(collision.transform);
        }

        if (collision.GetComponent<Draggable>() && !collision.GetComponent<Draggable>().IsHeld)
        {
            overlappingObj.Add(collision.transform);
        }
            
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Draggable>() && !collision.GetComponent<Draggable>().IsHeld)
        {
            if(!overlappingObj.Contains(collision.transform))
                overlappingObj.Add(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (overlappingObj.Contains(collision.transform))
        {
            overlappingObj.Remove(collision.transform);
        }
    }

}
