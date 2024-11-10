using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Transform cameraRoot;

    public float mouseSensitivity = 4f;
    public float pitchClamp = 60f;
    public float yaw2GoalSpeed = 5f;

    private float pitch = 0f, yaw = 0f, yawGoal = 0f;

    void Start() {

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {

        yawGoal += Input.GetAxis("Mouse X") * 4f;
        yaw = Mathf.Lerp(yaw, yawGoal, yaw2GoalSpeed * Time.deltaTime);

        pitch -= Input.GetAxis("Mouse Y") * 4f;
        pitch = Mathf.Clamp(pitch, -pitchClamp, pitchClamp);
    }

    void LateUpdate() {

        // remember, the camera rotation is ABSOLUTE, not relative to body rotation!
        cameraRoot.rotation = Quaternion.AngleAxis(yawGoal, Vector3.up) * Quaternion.AngleAxis(pitch, Vector3.right);
        
        // set body rotation (this gets lerped in update)
        transform.rotation = Quaternion.AngleAxis(yaw, Vector3.up);
    }
}
