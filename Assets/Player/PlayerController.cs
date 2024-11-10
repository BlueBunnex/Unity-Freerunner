using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Animation")]
    public Animator anim;
    public Transform modelRoot;
    public float yaw2GoalSpeed = 5f;

    [Header("Camera")]
    public Transform cameraRoot;
    public float mouseSensitivity = 4f;
    public float pitchClamp = 60f;

    [Header("Movement Tuning")]
    public CharacterController cc;
    public float acceleration = 5f;
    public float maxSpeed = 10f;
    public float gravity = -20f;
    public float jumpSpeed = 10f;

    // mouse
    private float pitch = 0f, yaw = 0f, yawGoal = 0f;

    // move
    private Vector3 movePlanarGlobal;
    private float moveVertical;

    void Start() {

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {

        UpdateCamera();
        UpdateMovement();
    }

    void LateUpdate() {

        // remember, the camera rotation is ABSOLUTE, not relative to our rotation!
        cameraRoot.rotation = Quaternion.AngleAxis(yawGoal, Vector3.up) * Quaternion.AngleAxis(pitch, Vector3.right);
        
        // set model rotation (this gets lerped in update)
        modelRoot.rotation = Quaternion.AngleAxis(yaw, Vector3.up);
    }

    void UpdateCamera() {

        yawGoal += Input.GetAxis("Mouse X") * 4f;
        yaw = Mathf.Lerp(yaw, yawGoal, yaw2GoalSpeed * Time.deltaTime);

        pitch -= Input.GetAxis("Mouse Y") * 4f;
        pitch = Mathf.Clamp(pitch, -pitchClamp, pitchClamp);

        transform.rotation = Quaternion.AngleAxis(yawGoal, Vector3.up);
    }

    void UpdateMovement() {

        bool airborne = !cc.isGrounded;

        anim.SetBool("Airborne", airborne);

        // apply input to planar movement
        movePlanarGlobal += ( transform.right * Input.GetAxis("Horizontal")
                            + transform.forward * Input.GetAxis("Vertical")) * acceleration * Time.deltaTime;

        // limit planar speed
        if (movePlanarGlobal.magnitude > maxSpeed) {

            movePlanarGlobal = movePlanarGlobal.normalized * maxSpeed;
        }

        // apply planar drag if not inputing
        if (!airborne && !Input.GetButton("Horizontal") && !Input.GetButton("Vertical")) {
            
            if (movePlanarGlobal.magnitude > 1f) {
                movePlanarGlobal = movePlanarGlobal.normalized * (movePlanarGlobal.magnitude - acceleration * Time.deltaTime);
            } else {
                movePlanarGlobal *= 0f;
            }
        }

        // animation from planar movement
        Vector3 movePlanarLocal = Quaternion.Inverse(transform.rotation) * movePlanarGlobal;

        anim.SetFloat("MoveVertical", movePlanarLocal.z / maxSpeed);
        anim.SetFloat("MoveHorizontal", movePlanarLocal.x / maxSpeed);

        // jumping & gravity
        if (airborne) {

            moveVertical += gravity * Time.deltaTime;

        } else {

            moveVertical = -1f; // pressing force

            if (Input.GetButton("Jump")) {
            
                moveVertical = jumpSpeed;
            }
        }

        cc.Move((movePlanarGlobal + moveVertical * Vector3.up) * Time.deltaTime);
    }
}
