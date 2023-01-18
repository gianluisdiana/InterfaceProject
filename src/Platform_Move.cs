using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Move : MonoBehaviour {
    // ------------------------- Serialized values ------------------------- //

    [Tooltip("Platform")]
    [SerializeField] private Transform parent;

    [Tooltip("Box")]
    [SerializeField] private Transform oldParent;

    // ------------------------- Private attributes ------------------------- //

    /// <value> To check if the platform has to be moved forward or not.  </value>
    private bool goForward;

    /// <value> To check if the platform has to be moved to the left or not. </value>
    private bool goLeft;

    /// <value> To check if the platform has reached its final position. </value>
    private bool finalPos;

    /// <value> Get the box object </value>
    private GameObject box;

    /// <value> Inicial position of the platform </value>
    private Vector3 initialPosition;

    /// <value> Position of the platform once it has gone forward </value>
    private Vector3 frontPosition;

    /// <value> Last position of the platform (after going left) </value>
    private Vector3 finalPosition;

    // ---------------------------- Private Method --------------------------- //

    /// <summary>
        /// Code of the movement of the platform. First it goes forward, then left till the final position.
    /// </summary>
    void move() {
        Vector3 currentPosition = transform.position;

        if (finalPos) return;

        if (goForward) {
            if (currentPosition.z < frontPosition.z) {
                transform.Translate(Vector3.forward * 0.01f);
            } else {
                goForward = false;
                goLeft = true;
            }
        } else if (goLeft) {
            if (currentPosition.x > finalPosition.x) {
                transform.Translate(Vector3.left * 0.01f);
            } else {
                goLeft = false;
            }
        } else {
            transform.Rotate(0, 0, 90);
            finalPos = true;
        }
    }

    // ----------------------------- Unity methods ----------------------------- //

    /// <summary>
        /// Initialises the private attributes
    /// </summary>
    void Start() {
        goForward = true;
        goLeft = false;
        finalPos = false;
        box = GameObject.FindWithTag("canPickUp");

        initialPosition =  = new Vector3(-1, 1.2f, -2.75f);
        frontPosition = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z + 5.5f);
        finalPosition = new Vector3(initialPosition.x - 2, initialPosition.y, initialPosition.z);
    }

    /// <summary>
        /// While the object stills colliding, the platform moves.
    /// </summary>
    /// <param name="collision"> The object that collided with. </param>
    void OnCollisionStay(Collision collision) {
        if (collision.gameObject.tag != box.tag) return;
        box.transform.SetParent(parent);
        move();
    }

    /// <summary>
        /// If the object is not longer colliding, the platform goes to its inicial position.
    /// </summary>
    void OnCollisionExit() {
        box.transform.SetParent(oldParent);
        transform.position = initialPosition;
        goForward = true;
        goLeft = false;
    }
}
