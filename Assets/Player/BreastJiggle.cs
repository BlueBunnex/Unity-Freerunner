using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreastJiggle : MonoBehaviour {

    public float dampen     = 6f;
    public float elasticity = 2f;

    private Quaternion restingRotationLocal; // Stays the same, used to determine acceleration
    private Quaternion lastRotationGlobal;   // So that animation is accounted for
    private Quaternion rotationalVelocity;   // current - resting = velocity, then update current

    void Start() {

        restingRotationLocal = transform.localRotation;

        lastRotationGlobal = transform.rotation;
    }

    void LateUpdate() {

        // load rotation
        transform.rotation = lastRotationGlobal;

        // dampen rotational velocity
        rotationalVelocity = Quaternion.Lerp(rotationalVelocity, Quaternion.identity, dampen * Time.deltaTime);

        // accelerate rotational velocity towards resting rotation
        Quaternion restingRotationGlobal = transform.parent.rotation * restingRotationLocal;

        Quaternion difference = Quaternion.Inverse(transform.rotation) * restingRotationGlobal;
        Quaternion acceleration = Quaternion.Lerp(Quaternion.identity, difference, elasticity * Time.deltaTime);

        rotationalVelocity = rotationalVelocity * acceleration;

        // update rotation with rotationalVelocity
        transform.rotation *= rotationalVelocity;

        // save rotation
        lastRotationGlobal = transform.rotation;
    }
}
