using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Move : MonoBehaviour {
    // ------------------------- Serialized values ------------------------- //

    [Tooltip("")]
    [SerializeField] private Transform parent;

    [Tooltip("")]
    [SerializeField] private Transform oldParent;

    // ------------------------- Private attributes ------------------------- //

    /// <value>  </value>
    private bool goForward;

    /// <value>  </value>
    private bool goLeft;

    /// <value>  </value>
    private bool finalPos;

    /// <value>  </value>
    private GameObject box;

    /// <value>  </value>
    private Vector3 initialPosition;

    /// <value>  </value>
    private Vector3 frontPosition;

    /// <value>  </value>
    private Vector3 finalPosition;

    // ---------------------------- Private Method --------------------------- //

    /// <summary>
        ///
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
        ///
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
        ///
    /// </summary>
    /// <param name="collision"> The object that collided with. </param>
    void OnCollisionStay(Collision collision) {
        if (collision.gameObject.tag != box.tag) return;
        box.transform.SetParent(parent);
        move();
    }

    /// <summary>
        ///
    /// </summary>
    void OnCollisionExit() {
        box.transform.SetParent(oldParent);
        transform.position = initialPosition;
        goForward = true;
        goLeft = false;
    }
}
