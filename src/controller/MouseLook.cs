using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {
  // ---- Serialized values (are visible in the inspector) ---- //
  [Tooltip("A multiplier to the input. Describes the maximum speed in degrees / second. To flip vertical rotation, set Y to a negative value")]
  [SerializeField] private Vector2 sensitivity;

  [Tooltip("The rotation acceleration, in degrees / second")]
  [SerializeField] private Vector2 acceleration;

  [Tooltip("The maximum angle from the horizon the player can rotate, in degrees")]
  [SerializeField] private float maxVerticalAngleFromHorizon = 90f;

  /// <value> Describes which directions this script should control. </value>
  [Flags] public enum RotationDirection {
    None,
    Horizontal = (1 << 0),
    Vertical = (1 << 1)
  }

  [Tooltip("Which directions this object can rotate")]
  [SerializeField] private RotationDirection rotationDirections;

  // --------------------- Private values --------------------- //

  ///<value> The current rotation, in degrees.</value>
  private Vector2 rotation;

  /// <value> The current rotation velocity, in degrees</value>
  private Vector2 velocity;


  // --------------------- Private methods --------------------- //

  /// <summary>
    /// Clamps the vertical angle to the maximum angle from the horizon.
  /// </summary>
  /// <param name="angle">The angle to clamp, in degrees</param>
  /// <returns>The clamped angle, in degrees</returns>
  private float ClampVerticalAngle(float angle) {
    return Mathf.Clamp(angle, -maxVerticalAngleFromHorizon, maxVerticalAngleFromHorizon);
  }

  /// <summary>
    /// Gets the mouse position in the X and Y axes.
  /// </summary>
  /// <returns>The mouse input.</returns>
  private Vector2 GetMouseInput() {
    // TODO: Change to work with the new input system (VR)
    Vector2 input = new Vector2(
      Input.GetAxis("Mouse X"),
      -Input.GetAxis("Mouse Y")
    );

    return input;
  }

  private void Update() {
    // The wanted velocity is the current input scaled by the sensitivity
    // This is also the maximum velocity
    Vector2 wantedVelocity = GetMouseInput() * sensitivity;

    // Zero out the wanted velocity if this controller does not rotate in that direction
    if ((rotationDirections & RotationDirection.Horizontal) == 0) {
        wantedVelocity.x = 0;
    }
    if ((rotationDirections & RotationDirection.Vertical) == 0) {
        wantedVelocity.y = 0;
    }

    // Calculate new rotation (smoother than just adding the velocity)
    velocity = new Vector2(
      Mathf.MoveTowards(velocity.x, wantedVelocity.x, acceleration.x * Time.deltaTime),
      Mathf.MoveTowards(velocity.y, wantedVelocity.y, acceleration.y * Time.deltaTime));
    rotation += velocity * Time.deltaTime;
    rotation.y = ClampVerticalAngle(rotation.y);

    // Convert the rotation to euler angles
    transform.localEulerAngles = new Vector3(rotation.y, rotation.x, 0);
  }
}