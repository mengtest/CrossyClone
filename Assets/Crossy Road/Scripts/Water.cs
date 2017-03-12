using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    private bool hitWater = false;

    private void OnTriggerStay(Collider other) {
        // If the player hit the water, get out of here
        if(hitWater) return;

        if(other.CompareTag("Player")) {
            // Get the player controller
            PlayerController playerController = other.GetComponent<PlayerController>();

            if(!playerController.parentedToObject && playerController.state != PlayerController.State.Moving) {
                hitWater = true;
                playerController.GotSoaked();
            }
        }
    }
}
