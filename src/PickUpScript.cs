using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour {
    // ------------------------- Serialized values ------------------------- //

    [Tooltip("The player object")]
    [SerializeField] private GameObject player;

    [Tooltip("The position where the object will be held")]
    [SerializeField] private Transform holdPosition;

    [Tooltip("How much force is applied to the object when thrown")]
    [SerializeField] private float throwForce = 500f;

    [Tooltip("How far the player can pickup the object from")]
    [SerializeField] public float pickUpRange = 5f;

    // ------------------------- Private attributes ------------------------- //

    /// <value> Object which we pick up. </value>
    private GameObject _heldObj;

    /// <value> Rigidbody of object we pick up. </value>
    private Rigidbody _heldObjRb;

    /// <value> Layer index. </value>
    private int _layerNumber;

    // ------------------------------ Notifier ------------------------------ //

    /// <summary>
        /// Type of message to send to the subscribers.
    /// </summary>
    public delegate void ObjectState();

    /// <summary>
        /// Events that will be triggered when the object is grabbed or dropped.
    /// </summary>
    public event ObjectState OnGrab;
    public event ObjectState OnDrop;

    // ------------------------------ Subscriber ------------------------------ //

    [Tooltip("The user interactions script to subscribe to")]
    [SerializeField] private Interactions userNotifier;

    // --------------------------- Private Methods --------------------------- //

# region Interactions with the object

    /// <summary>
        /// Pick up an object and lock it to the player
    /// </summary>
    private void PickUpObject (GameObject pickUpObj) {
        if (!(pickUpObj.GetComponent<Rigidbody>())) return; // Make sure the object has a RigidBody

        _heldObj = pickUpObj; //assign heldObj to the object that was hit by the ray-cast (no longer == null)
        _heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
        _heldObjRb.isKinematic = true;
        _heldObjRb.transform.parent = holdPosition.transform; //parent object to hold position
        _heldObj.layer = _layerNumber; //change the object layer to the holdLayer
        //make sure object doesnt collide with player, it can cause weird bugs
        Physics.IgnoreCollision(_heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        OnGrab();
    }

    /// <summary>
        /// Try to pick up an object in front of the player
    /// </summary>
    private void TryToPickUpObject () {
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange)) return;

        // Make sure pickup tag is attached
        if (hit.transform.gameObject.tag != "canPickUp") return;

        // Pass in object hit into the PickUpObject function
        PickUpObject(hit.transform.gameObject);
    }

    /// <summary>
        /// Prevents object from clipping through walls.
        /// Only called when dropping / throwing.
    /// </summary>
    private void StopClipping() {
        var clipRange = Vector3.Distance(_heldObj.transform.position, transform.position); // Distance from "holdPosition" to the camera
        //have to use RaycastAll as object blocks ray-cast in center screen
        //RaycastAll returns array of all colliders hit within the clip-range
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        //if the array length is greater than 1, meaning it has hit more than just the object we are carrying
        if (hits.Length > 1) {
            //change object position to camera position
            _heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f); //offset slightly downward to stop object dropping above player 
            //if your player is small, change the -0.5f to a smaller number (in magnitude) ie: -0.1f
        }
    }

    /// <summary>
        /// Drop the object and make it fall to the ground
    /// </summary>
    private void DropObject () {
        if (_heldObj == null) return;
        StopClipping(); // Prevents object from clipping through walls

        // Re-enable collision with player
        Physics.IgnoreCollision(_heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        _heldObj.layer = 0; // Object assigned back to default layer
        _heldObjRb.isKinematic = false;
        _heldObj.transform.parent = null; // Un-parent object
        _heldObj = null; // Undefine game object attached to _heldObj
        OnDrop();
    }

    /// <summary>
        /// Same as drop function, but add force to object before undefining it
    /// </summary>
    private void ThrowObject() {
        if (_heldObj == null) return;

        float actualThrowForce = throwForce;
        actualThrowForce *= (_heldObj.name == "Ball" ? 1.5f : 1);

        DropObject();
        _heldObjRb.AddForce(transform.forward * actualThrowForce);
    }

    /// <summary>
        /// Keep the object position the same as the holdPosition position
    /// </summary>
    private void MoveObject () {
        _heldObj.transform.position = holdPosition.transform.position;
    }

# endregion

    // ---------------------------- Unity methods ---------------------------- //

    /// <summary>
        /// Set the functions to be called when a certain button is pressed.
    /// </summary>
    private void Start () {
        _layerNumber = LayerMask.NameToLayer("holdLayer");
        userNotifier.OnCirclePressed += TryToPickUpObject;
        userNotifier.OnSquarePressed += DropObject;
        userNotifier.OnXPressed += ThrowObject;
    }

    /// <summary>
        /// If the user is holding an object, keep moving it with that user.
    /// </summary>
    private void Update() {
        if (_heldObj == null) return;
        MoveObject(); // Keep the object position at 'holdPosition'
    }
}