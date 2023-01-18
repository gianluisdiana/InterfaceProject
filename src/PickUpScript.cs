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

    private GameObject heldObj;  // Object which we pick up
    private Rigidbody heldObjRb; // Rigidbody of object we pick up

    private int LayerNumber;     // Layer index

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

        heldObj = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
        heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
        heldObjRb.isKinematic = true;
        heldObjRb.transform.parent = holdPosition.transform; //parent object to hold position
        heldObj.layer = LayerNumber; //change the object layer to the holdLayer
        //make sure object doesnt collide with player, it can cause weird bugs
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
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
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position); // Distance from "holdPosition" to the camera
        //have to use RaycastAll as object blocks raycast in center screen
        //RaycastAll returns array of all colliders hit within the cliprange
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        //if the array length is greater than 1, meaning it has hit more than just the object we are carrying
        if (hits.Length > 1) {
            //change object position to camera position
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f); //offset slightly downward to stop object dropping above player 
            //if your player is small, change the -0.5f to a smaller number (in magnitude) ie: -0.1f
        }
    }

    /// <summary>
        /// Drop the object and make it fall to the ground
    /// </summary>
    private void DropObject () {
        if (heldObj == null) return;
        StopClipping(); // Prevents object from clipping through walls

        // Re-enable collision with player
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0; // Object assigned back to default layer
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null; // Unparent object
        heldObj = null; // Undefine game object attached to heldObj
        OnDrop();
    }

    /// <summary>
        /// Same as drop function, but add force to object before undefining it
    /// </summary>
    private void ThrowObject() {
        if (heldObj == null) return;

        float actualThrowForce;
        if (heldObj.name == "Ball")  {
            actualThrowForce = throwForce * 1.5f;
        } else {
            actualThrowForce = throwForce;
        }

        DropObject();
        heldObjRb.AddForce(transform.forward * actualThrowForce);
    }

    /// <summary>
        /// Keep the object position the same as the holdPosition position
    /// </summary>
    private void MoveObject () {
        heldObj.transform.position = holdPosition.transform.position;
    }

# endregion

    private void Start () {
        LayerNumber = LayerMask.NameToLayer("holdLayer");
        userNotifier.OnGPressed += TryToPickUpObject;
        userNotifier.OnDPressed += DropObject;
        userNotifier.OnTPressed += ThrowObject;
    }

    private void Update() {
        if (heldObj == null) return;
        MoveObject(); // Keep the object position at 'holdPosition'
    }
}