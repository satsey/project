using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicBody : MonoBehaviour {
  public Vector3 currentConstantForce;
  public Vector3 currentVelocity;
  public float horizontalDrag = -0.05f;
  public float verticalDrag = -0.05f;
  public bool useCharacterController;
  public bool autoMove;

  CharacterController characterController;

  void Awake() {
    //  QualitySettings.vSyncCount = 0;  // VSync must be disabled
     Application.targetFrameRate = -1;
    if (useCharacterController) {
      characterController = GetComponent<CharacterController>();
    }
  }

  void LateUpdate() {
    if (autoMove) {
      DoMove();
    }
  }

  public void DoMove() {
    currentVelocity = PhysicsCalc.GetNextVelVector3(currentConstantForce, currentVelocity, horizontalDrag, verticalDrag, Time.deltaTime);
    Vector3 wantedDeltaPos = PhysicsCalc.GetDeltaPosVector3(currentConstantForce, currentVelocity, horizontalDrag, verticalDrag, Time.deltaTime);


    if (useCharacterController) {
      MoveWithCharaController(wantedDeltaPos);
    } else {
      transform.position += wantedDeltaPos;
    }
  }

  void MoveWithCharaController(Vector3 wantedDeltaPos) {
    Vector3 prevPosition = transform.position;

    characterController.Move(wantedDeltaPos);
    Vector3 actualDeltaPos = transform.position - prevPosition;

    float actualVelX = GetActualVelocity(actualDeltaPos.x, wantedDeltaPos.x, currentVelocity.x);
    float actualVelY = GetActualVelocity(actualDeltaPos.y, wantedDeltaPos.y, currentVelocity.y);
    float actualVelZ = GetActualVelocity(actualDeltaPos.z, wantedDeltaPos.z, currentVelocity.z);

    currentVelocity = new Vector3(actualVelX, actualVelY, actualVelZ);
  }

  float GetActualVelocity(float actualDeltaPos, float wantedDeltaPos, float wantedVel) {
    float roundedActual = Mathf.Round(actualDeltaPos * 100) / 100;
    float roundedWanted = Mathf.Round(wantedDeltaPos * 100) / 100;

    if ((roundedWanted > 0 && roundedActual < roundedWanted) || (roundedWanted < 0 && roundedActual > roundedWanted)) {
      return LimitValue(actualDeltaPos, wantedDeltaPos) / Time.deltaTime;
    }

    return wantedVel;
  }

  float LimitValue(float val, float max) {
    if (max > 0) {
      return Mathf.Clamp(val, 0, max);
    }

    return Mathf.Clamp(val, max, 0);
  }

}
