using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInformation : MonoBehaviour
{
    public Vector2 maxCameraPosition;
    public Vector2 minCameraPosition;
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(maxCameraPosition, new Vector3(maxCameraPosition.x, minCameraPosition.y));
        Gizmos.DrawLine(maxCameraPosition, new Vector3(minCameraPosition.x, maxCameraPosition.y));
        Gizmos.DrawLine(minCameraPosition, new Vector3(maxCameraPosition.x, minCameraPosition.y));
        Gizmos.DrawLine(minCameraPosition, new Vector3(minCameraPosition.x, maxCameraPosition.y));
    }
}
