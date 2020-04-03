using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    void Start()
    {
        if (!this.gameObject.name.Equals("SpawnPoint0")) //if the spawnpoint is in the first room
            DisableSpawner(); //all other spawn point in other rooms will be inactive
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            EnableSpawner();
    }
    void OnTriggerExit2D(Collider2D other)
    {
        DisableSpawner();
    }
    void DisableSpawner()
    {
        int numEnemies = this.gameObject.transform.childCount; //get the number of enemies belonging to the spawner (the gameObject's children) and disable them
        for (int i = 0; i < numEnemies; i++)
        {
            this.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    void EnableSpawner()
    {
        int numEnemies = this.gameObject.transform.childCount;
        for (int i = 0; i < numEnemies; i++)
        {
            this.gameObject.transform.GetChild(i).gameObject.SetActive(true);
            //this.gameObject.transform.GetChild(i).gameObject.GetComponent<EnemyAI>().health = 100f; //resets the health of enemies that were not killed to 100
        }
    }
}
