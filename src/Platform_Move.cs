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

    /// <value> To check if the platform has to be moved forward or not. </value>
    private bool _goForward;

    /// <value> To check if the platform has to be moved to the left or not. </value>
    private bool _goLeft;

    /// <value> To check if the platform has reached its final position. </value>
    private bool _finalPos;

    /// <value> Get the box object. </value>
    private GameObject _box;

    /// <value> Inicial position of the platform. </value>
    private Vector3 _initialPosition;

    /// <value> Position of the platform once it has gone forward. </value>
    private Vector3 _frontPosition;

    /// <value> Last position of the platform (after going left). </value>
    private Vector3 _finalPosition;

    // ---------------------------- Private Method --------------------------- //

    /// <summary>
        /// Move the platform. First it goes forward, then left till the final position.
    /// </summary>
    void move() {
        Vector3 currentPosition = transform.position;

        if (_finalPos) return;

        if (_goForward) {
            if (currentPosition.z < _frontPosition.z) {
                transform.Translate(Vector3.forward * 0.01f);
            } else {
                _goForward = false;
                _goLeft = true;
            }
        } else if (_goLeft) {
            if (currentPosition.x > _finalPosition.x) {
                transform.Translate(Vector3.left * 0.01f);
            } else {
                _goLeft = false;
            }
        } else {
            transform.Rotate(0, 0, 90);
            _finalPos = true;
        }
    }

    // ----------------------------- Unity methods ----------------------------- //

    /// <summary>
        /// Initializes the private attributes
    /// </summary>
    void Start() {
        _goForward = true;
        _goLeft = false;
        _finalPos = false;
        _box = GameObject.FindWithTag("canPickUp");

        _initialPosition =  = new Vector3(-1, 1.2f, -2.75f);
        _frontPosition = new Vector3(_initialPosition.x, _initialPosition.y, _initialPosition.z + 5.5f);
        _finalPosition = new Vector3(_initialPosition.x - 2, _initialPosition.y, _initialPosition.z);
    }

    /// <summary>
        /// While the object stills colliding, the platform moves.
    /// </summary>
    /// <param name="collision"> The object that collided with. </param>
    void OnCollisionStay(Collision collision) {
        if (collision.gameObject.tag != _box.tag) return;
        _box.transform.SetParent(parent);
        move();
    }

    /// <summary>
        /// If the object is not longer colliding, the platform goes to its inicial position.
    /// </summary>
    void OnCollisionExit() {
        _box.transform.SetParent(oldParent);
        transform.position = _initialPosition;
        _goForward = true;
        _goLeft = false;
    }
}
