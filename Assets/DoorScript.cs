using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class DoorScript : MonoBehaviour
{
    public GameObject doorSpawnPoint;
    public Tilemap DestinationDoor; //we need to call DestinationDoor and doorSpawnPoint separately because doorSpawnPoint is for spawning directly on the door area 
                                    //(We can't change the position of the Door Tilemap to solve the issue of the player spawning in the center of the next room)
                                    //and we need DestinationDoor so we can access the "transportedTo" Boolean of the destination door's script  and
                                    // make sure once the player gets transported to the next Door, it won't immediately transport them back
    public bool transportedTo; //if the player was transported to this door, we set the value of this to "true" so that the player doesn't get stuck transporting between the two doors
    // Start is called before the first frame update
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !transportedTo)
        {
            DestinationDoor.GetComponent<DoorScript>().transportedTo = true;
            other.gameObject.transform.position = doorSpawnPoint.transform.position;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        transportedTo = false;
    }
}
