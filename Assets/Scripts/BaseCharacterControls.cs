using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterControls : MonoBehaviour {
  public CharacterController characterController;
  public KinematicBody kinematicBody;
  public Camera mainCamera;
  public float mouseHorizontalSensitivity;
  public float mouseVerticalSensitivity;

  public float walkSpeed;
  public float airWalkSpeed;

  public float groundDrag;
  public float airHorizontalDrag;
  public float airVerticalDrag;

  public float gravity;

  public float jumpSpeed;

  
  float verticalViewingAngle = 0;

  void Start() {
    Cursor.lockState = CursorLockMode.Locked;

  }


  void Update() {
    CameraControls();

    // Implementing (infinite) jumps, when pressing the Space bar
    if (Input.GetKeyDown(KeyCode.Space)) {
      float yVel = kinematicBody.currentVelocity.y;

      //If already moving up, add jump to current velocity (yVel + jumpSpeed). If falling down, add the jump as if the player is stationary (0 + jumpSpeed).
      yVel = Mathf.Max(yVel + jumpSpeed, 0 + jumpSpeed);
      
      //Apply new velocity to the KinematicBody (it's a custom script).
      kinematicBody.currentVelocity = new Vector3(kinematicBody.currentVelocity.x, yVel, kinematicBody.currentVelocity.z);
    }

    PlayerMovementUpdate();
  }


  void CameraControls() {
    //Get the delta X and Y for the mouse movement.
    float rotateX = Input.GetAxis("Mouse X") * mouseHorizontalSensitivity;
    float rotateY = Input.GetAxis("Mouse Y") * mouseVerticalSensitivity;


    // When moving the camera horizontally, rotate the whole body. The forward direction changes.
    transform.Rotate(Vector3.up * rotateX);

    // When moving the camera vertically, only rotate the camera.
    // Clamping the vertical angle [-90, 90], so the player can't look up or down beyond limits
    verticalViewingAngle = Mathf.Clamp(verticalViewingAngle - rotateY, -90f, 90f);
    mainCamera.transform.localRotation = Quaternion.Euler(verticalViewingAngle, 0, 0);
  }
  
  void PlayerMovementUpdate() {
    // x: [-1, 1], y: [-1, 1]
    Vector2 controlMovementAxis = GetControlMovementAxis();

    // Translate the control into an actual world direction.
    Vector3 movementAbsoluteDirection = (transform.right * controlMovementAxis.x + transform.forward * controlMovementAxis.y).normalized;

    // The current force could be zero. If floating in space with no gravity
    Vector3 currentConstantForce = Vector3.zero;

    // Here I'm adding gravity to push down
    currentConstantForce += Vector3.down * gravity;

    if (IsGrounded()) {
      // Walking is just a constant force towards the walking direction. I add it to the constant force (which already includes gravity).
      currentConstantForce += movementAbsoluteDirection * walkSpeed;
    } else {
      // In the air you might want to accelerate differently. The airwalking force should be lower than when on the ground.
      currentConstantForce += movementAbsoluteDirection * airWalkSpeed;
    }

    // I'm done with the constant force, so I apply it to my KinematicBody (custom script).
    kinematicBody.currentConstantForce = currentConstantForce;

    if (IsGrounded()) {
      //On the ground, the drag is higher (the velocity get pulled to zero faster)
      kinematicBody.horizontalDrag = groundDrag;
      kinematicBody.verticalDrag = groundDrag;
    } else {
      //In the air, the drag is lower (the velocity get pulled to zero slower)
      kinematicBody.horizontalDrag = airHorizontalDrag;
      kinematicBody.verticalDrag = airVerticalDrag;
    }

    // Once currentConstantForce, currentVelocity, and drag is all set, it's time to do the computation.
    // Do move will calculate the new currenVelocity, and the new position.
    kinematicBody.DoMove();
  }

  Vector2 GetControlMovementAxis() {
    return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
  }

  bool IsGrounded() {
    //If I'm touching the ground AND haven't started to ascend.
    //I want to be considered airborne the frame I'm jumping, so the jump is consistent
    return characterController.isGrounded == true && kinematicBody.currentVelocity.y <= 0f;
  }

}
