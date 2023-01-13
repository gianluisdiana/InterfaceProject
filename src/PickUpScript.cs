using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour {
    public GameObject player;
    public Transform holdPos;
    public float throwForce = 500f; //force at which the object is thrown at
    public float pickUpRange = 5f; //how far the player can pickup the object from

    private GameObject heldObj; //object which we pick up
    private Rigidbody heldObjRb; //rigidbody of object we pick up

    private int LayerNumber; //layer index

    public delegate void GrabState();

    public event GrabState OnGrab;
    public event GrabState OnNotGrab;

    // ------------------------------ Subscriber ------------------------------ //
    [Tooltip("The voice recognition script to subscribe to")]
    [SerializeField] private VoiceRecognition voiceNotifier;


    // --------------------------- Private Methods --------------------------- //

    /// <summary>
        /// Pick up an object and lock it to the player
    /// </summary>
    private void PickUpObject (GameObject pickUpObj) {
        if (!(pickUpObj.GetComponent<Rigidbody>())) return; // Make sure the object has a RigidBody

        heldObj = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
        heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
        heldObjRb.isKinematic = true;
        heldObjRb.transform.parent = holdPos.transform; //parent object to holdposition
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
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position); // Distance from holdPos to the camera
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
        OnNotGrab();
    }

    /// <summary>
        /// Same as drop function, but add force to object before undefining it
    /// </summary>
    private void ThrowObject() {
        if (heldObj == null) return;

        DropObject();
        heldObjRb.AddForce(transform.forward * throwForce);
    }

    /// <summary>
        /// Keep the object position the same as the holdPosition position
    /// </summary>
    private void MoveObject () {
        heldObj.transform.position = holdPos.transform.position;
    }

    private void Start () {
        LayerNumber = LayerMask.NameToLayer("holdLayer");
        voiceNotifier.OnGrabSaid += TryToPickUpObject;
        voiceNotifier.OnDropSaid += DropObject;
        voiceNotifier.OnThrowSaid += ThrowObject;
    }

    private void Update() {
        if (heldObj == null) return;
        MoveObject(); // Keep the object position at 'holdPos'
    }
}
