using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceArea : MonoBehaviour {

    public float bounceSpeed = 10f;
    public bool bounceOffNotUp = false;

    private void OnTriggerEnter(Collider other) {
        
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null) {

            if (bounceOffNotUp) {

                Vector3 offVector = transform.forward;

                player.moveVertical = offVector.y * bounceSpeed;
                player.movePlanarGlobal = (Vector3.right * offVector.x + Vector3.forward * offVector.z) * bounceSpeed;

            } else {

                player.moveVertical = bounceSpeed;
            }
        }
    }
}
