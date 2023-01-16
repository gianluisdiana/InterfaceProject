using UnityEngine;

[System.Serializable] public struct Materials {
    public string name;
    public Material defaultMaterial;
    public Material highlightMaterial;
}

public class SelectionManager : MonoBehaviour {
    // ------------------------- Serialized values ------------------------- //

    [Tooltip("The materials that will be used to highlight the objects.")]
    [SerializeField] private Materials[] materials;

    [Tooltip("The script that will notify when an object is grabbed.")]
    [SerializeField] private PickUpScript notifier;

    // ------------------------- Private attributes ------------------------- //

    /// <value> Whether an object is currently <c>grabbed</c>. </value>
    private bool grabbed;

    /// <value> The object that is currently <c>selected</c>. </value>
    private Transform objectInVision;

    // --------------------------- Private methods --------------------------- //

    private void objectGrabbed() {
        grabbed = true;
    }

    private void objectNotGrabbed() {
        grabbed = false;
    }

    private void Start() {
        grabbed = false;
        notifier.OnGrab += objectGrabbed;
        notifier.OnDrop += objectNotGrabbed;
    }

    /// <summary>
        /// Gets the material that corresponds to the object's name.
    /// </summary>
    /// <param name="name"> The name of the object to look for. </param>
    /// <param name="highlight"> Whether the material should be highlighted or not. </param>
    /// <returns> The material that corresponds to the object's name. </returns>
    private Material GetMaterial(string name, bool highlight) {
        foreach (Materials objMat in this.materials) {
            if (objMat.name == name) {
                return highlight ? objMat.highlightMaterial : objMat.defaultMaterial;
            }
        }
        return null;
    }

    private void Update() {
        if (objectInVision != null) {
            Renderer selectionRenderer = objectInVision.GetComponent<Renderer>();
            selectionRenderer.material = GetMaterial(objectInVision.name, false);
            objectInVision = null;
        }

        RaycastHit hit;
        // If the ray-cast does not hit anything, do not highlight anything.
        if (!(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward),
            out hit, notifier.pickUpRange))) return;

        Transform newObjectInVision = hit.transform;
        // If the object is not pick-able, do not highlight it.
        if (!newObjectInVision.CompareTag("canPickUp")) return;

        Renderer selectedRenderer = newObjectInVision.GetComponent<Renderer>();
        // If the user is holding the object, do not highlight it.
        if ((selectedRenderer == null) || (grabbed)) return;

        selectedRenderer.material = GetMaterial(newObjectInVision.name, true);
        objectInVision = newObjectInVision;
    }
}