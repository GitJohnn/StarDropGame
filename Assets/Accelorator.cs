using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelorator : MonoBehaviour {

    [SerializeField] float boostEffectLasting;
    [SerializeField] float speedMultiplier;
    float originalSpeed;
    float elapsedTime = 0;
    Movement player;


    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag.Equals("Player") && !other.GetComponent<Movement>().isJumping) {
            player = other.GetComponent<Movement>();
            if (player.CurrentSpeed == player.SPEED) {
                elapsedTime = 0;
                originalSpeed = other.GetComponent<Movement>().CurrentSpeed;
                other.GetComponent<Movement>().CurrentSpeed *= speedMultiplier;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        elapsedTime = 0;
    }

    private void Update() {
        if (elapsedTime >= boostEffectLasting) {
            if (player != null) {
                player.CurrentSpeed = player.SPEED;
            }
        } else {
            elapsedTime += Time.deltaTime;
        }
    }
}
