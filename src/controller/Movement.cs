using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

  /// <value>Property <c>tf</c> represents the <see cref="UnityEngine.CoreModule.Transform"> component.</value>
  private Transform tf;

  /// <value>The <c>speed</c> of the movement.</value>
  [field: SerializeField] public float speed = 5f;


  /// <summary>
    /// Moves itself in the XZ plane.
  /// </summary>
  private void Move() {
    float z_movement = Input.GetAxis("Vertical") * Time.deltaTime * speed;
    float x_movement = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
    tf.position += tf.forward * z_movement + tf.right * x_movement;
  }

  // Start is called before the first frame update
  void Start() {
    tf = GetComponent<Transform>();
  }

  /// <summary>
    /// Moves the object normally with the arrow keys
  /// </summary>
  void Update() {
    Move();
  }
}