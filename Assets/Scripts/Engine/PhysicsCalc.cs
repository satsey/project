using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCalc : MonoBehaviour {
  public static Vector3 GetNextAccVector3(Vector3 constantForce, Vector3 currentVel, float hDrag, float yDrag, float deltaTime) {
    float xAcc = GetNextAcc(constantForce.x, currentVel.x, hDrag, deltaTime);
    float yAcc = GetNextAcc(constantForce.y, currentVel.y, yDrag, deltaTime);
    float zAcc = GetNextAcc(constantForce.z, currentVel.z, hDrag, deltaTime);

    return new Vector3(xAcc, yAcc, zAcc);
  }

  static float GetNextAcc(float constantForce, float currentVel, float drag, float deltaTime) {
    float finalVel = constantForce / -drag;

    return drag * (currentVel - finalVel) * Mathf.Exp(drag * deltaTime);
  }



  public static Vector3 GetNextVelVector3(Vector3 constantForce, Vector3 currentVel, float hDrag, float yDrag, float deltaTime) {
    float xVel = GetNextVel(constantForce.x, currentVel.x, hDrag, deltaTime);
    float yVel = GetNextVel(constantForce.y, currentVel.y, yDrag, deltaTime);
    float zVel = GetNextVel(constantForce.z, currentVel.z, hDrag, deltaTime);

    return new Vector3(xVel, yVel, zVel);
  }

  public static float GetNextVel(float constantForce, float currentVel, float drag, float deltaTime) {
    float finalVel = constantForce / -drag;

    return finalVel + (currentVel - finalVel) * Mathf.Exp(drag * deltaTime);
  }



  public static Vector3 GetDeltaPosVector3(Vector3 constantForce, Vector3 currentVel, float hDrag, float yDrag, float deltaTime) {
    float xPos = GetDeltaPos(constantForce.x, currentVel.x, hDrag, deltaTime);
    float yPos = GetDeltaPos(constantForce.y, currentVel.y, yDrag, deltaTime);
    float zPos = GetDeltaPos(constantForce.z, currentVel.z, hDrag, deltaTime);

    return new Vector3(xPos, yPos, zPos);
  }

  static float GetDeltaPos(float constantForce, float currentVel, float drag, float deltaTime) {
    float finalVel = constantForce / -drag;

    float C = (currentVel - finalVel) / -drag;

    return C + finalVel * deltaTime - C * Mathf.Exp(drag * deltaTime);
  }

  public static float GetDeltaTimeFromVel(float constantForce, float currentVel, float drag, float targetVel) {
    float finalVel = constantForce / -drag;

    return Mathf.Log((targetVel - finalVel) / (currentVel - finalVel)) / drag;
  }
}
