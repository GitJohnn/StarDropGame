using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SolidWallTileSet : MonoBehaviour {

    [SerializeField] bool debug;
    public bool penetrable = false;

    // Start is called before the first frame update
    void Start() {
        if (debug) {
            Debug.Log(transform.name);
        }
    }

    // Update is called once per frame
    void Update() {
        Vector3 snapPos;
        snapPos.x = Mathf.RoundToInt(transform.position.x / 0.5f) * 0.5f;
        snapPos.y = Mathf.RoundToInt(transform.position.y / 0.5f) * 0.5f;
        transform.position = new Vector3(snapPos.x, snapPos.y, 0f);

        transform.name = "(" + snapPos.x + "," + snapPos.y + ")";
    }
}
