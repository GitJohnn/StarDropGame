using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public bool useBounds;
    private Vector2 maxCameraPosition;
    private Vector2 minCameraPosition;
    private RoomInformation currentRoom;
    public Transform playertransform;
    // Start is called before the first frame update
    void Start()
    {
        currentRoom = FindObjectOfType<RoomInformation>();
        maxCameraPosition = currentRoom.maxCameraPosition;
        minCameraPosition = currentRoom.minCameraPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 minScreenPoint = Camera.main.WorldToScreenPoint(minCameraPosition);
        Vector2 maxScreenPoint = Camera.main.WorldToScreenPoint(maxCameraPosition);
        Vector2 playerScreenPoint = Camera.main.WorldToScreenPoint(playertransform.position);

        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPosition.x, maxCameraPosition.x), Mathf.Clamp(transform.position.y, minCameraPosition.y, maxCameraPosition.y),-10);
        transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Mathf.Clamp(playerScreenPoint.x, minScreenPoint.x + (Screen.width/2), maxScreenPoint.x - (Screen.width/2)),
             Mathf.Clamp(playerScreenPoint.y, minScreenPoint.y + (Screen.height/2), maxScreenPoint.y - (Screen.height/2))));// + (Vector3.forward * -10);
    }
}
