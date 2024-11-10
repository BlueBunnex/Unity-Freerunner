using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreastJiggle : MonoBehaviour {

    private Quaternion restingRotationLocal;

    private Quaternion lastRotationGlobal;

    private Quaternion rotationalVelocity;

    void Start() {

        restingRotationLocal = transform.localRotation;

        lastRotationGlobal = transform.rotation;
    }

    void LateUpdate() {

        // load rotation
        transform.rotation = lastRotationGlobal;

        // dampen rotational velocity
        rotationalVelocity = Quaternion.Lerp(rotationalVelocity, Quaternion.identity, 2f * Time.deltaTime);

        // accelerate rotational velocity towards resting rotation
        Quaternion restingRotationGlobal = transform.parent.rotation * restingRotationLocal;

        Quaternion difference = Quaternion.Inverse(transform.rotation) * restingRotationGlobal;
        Quaternion acceleration = Quaternion.Lerp(Quaternion.identity, difference, 5f * Time.deltaTime);

        rotationalVelocity = rotationalVelocity * acceleration;

        // update rotation with rotationalVelocity
        transform.rotation *= rotationalVelocity;

        // save rotation
        lastRotationGlobal = transform.rotation;
    }
}
