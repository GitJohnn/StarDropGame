using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelorator : MonoBehaviour {

    [SerializeField] float effectTiming;
    [SerializeField] float speedMultiplier;
    float originalSpeed;
    float elapsedTime = 0;
    bool isAffected = false;
    Movement player;


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals("Player") && !other.GetComponent<Movement>().isJumping && !isAffected) {
            player = other.GetComponent<Movement>();
            originalSpeed = player.modSpeed;
            player.modSpeed *= speedMultiplier;
            Debug.Log(player.modSpeed);
            isAffected = true;
            StartCoroutine(BoostEffect(effectTiming));
        }
    }

    IEnumerator BoostEffect(float time)
    {
        yield return new WaitForSeconds(time);
        player.modSpeed = originalSpeed;
        isAffected = false;
    }

}
