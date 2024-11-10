using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceArea : MonoBehaviour {

    public float bounceSpeed = 10f;

    private void OnTriggerEnter(Collider other) {
        
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null) {
            player.moveVertical = bounceSpeed;
        }
    }
}
