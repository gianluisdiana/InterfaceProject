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

        // ------------------------------ Subscriber ------------------------------ //
        [Tooltip("The voice recognition script to subscribe to")]
        [SerializeField] private VoiceRecognition voiceNotifier;

        private void TryToPickUpObject () {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange)) {
                // Make sure pickup tag is attached
                if (hit.transform.gameObject.tag != "canPickUp") return;

                // Pass in object hit into the PickUpObject function
                PickUpObject(hit.transform.gameObject);
            }
        }

        void Start() {
            LayerNumber = LayerMask.NameToLayer("holdLayer");
            voiceNotifier.OnGrabSaid += TryToPickUpObject;
            voiceNotifier.OnDropSaid += DropObject;
            voiceNotifier.OnThrowSaid += ThrowObject;
        }

        void Update() {
            if (heldObj == null) return; //if player is holding object

            MoveObject(); //keep object position at holdPos
        }

        void PickUpObject(GameObject pickUpObj) {
            if (!(pickUpObj.GetComponent<Rigidbody>())) return; // Make sure the object has a RigidBody

            heldObj = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
            heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform; //parent object to holdposition
            heldObj.layer = LayerNumber; //change the object layer to the holdLayer
            //make sure object doesnt collide with player, it can cause weird bugs
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }

        void DropObject() {
            if (heldObj == null) return;

            StopClipping(); //prevents object from clipping through walls

            // Re-enable collision with player
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
            heldObj.layer = 0; //object assigned back to default layer
            heldObjRb.isKinematic = false;
            heldObj.transform.parent = null; //unparent object
            heldObj = null; //undefine game object
        }

        void MoveObject() {
            //keep object position the same as the holdPosition position
            heldObj.transform.position = holdPos.transform.position;
        }

        void ThrowObject() {
            if (heldObj == null) return;

            StopClipping();

            //same as drop function, but add force to object before undefining it
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
            heldObj.layer = 0;
            heldObjRb.isKinematic = false;
            heldObj.transform.parent = null;
            heldObjRb.AddForce(transform.forward * throwForce);
            heldObj = null;
        }

        void StopClipping() { //function only called when dropping/throwing
            var clipRange = Vector3.Distance(heldObj.transform.position, transform.position); //distance from holdPos to the camera
            //have to use RaycastAll as object blocks raycast in center screen
            //RaycastAll returns array of all colliders hit within the cliprange
            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
            //if the array length is greater than 1, meaning it has hit more than just the object we are carrying
            if (hits.Length > 1)
            {
                //change object position to camera position 
                heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f); //offset slightly downward to stop object dropping above player 
                //if your player is small, change the -0.5f to a smaller number (in magnitude) ie: -0.1f
            }
        }
    }