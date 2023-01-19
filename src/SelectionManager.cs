using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable] public struct Materials {
    public string name;
    public Material defaultMaterial;
    public Material highlightMaterial;
}

public class SelectionManager : MonoBehaviour {
    // ------------------------- Serialized values ------------------------- //

    [Tooltip("The materials that will be used to highlight the objects.")]
    [SerializeField] private Materials[] materials;

    // ----------------------------- Subscriber ----------------------------- //

    [Tooltip("The script that will notify when an object is grabbed.")]
    [SerializeField] private PickUpScript notifier;

    // ------------------------------ Notifier ------------------------------ //

    /// <summary>
        /// Sends a regular message to the subscribers when an object material is changed.
    /// </summary>
    public delegate void HighlightState();

    /// <summary>
        /// Events that will be triggered when an object is highlighted.
    /// </summary>
    public event HighlightState OnHighlight;

    // ------------------------- Private attributes ------------------------- //

    /// <value> Whether an object is currently <c>grabbed</c>. </value>
    private bool _grabbed;

    /// <value> The object that is currently <c>selected</c>. </value>
    private Transform _objectInVision;

    // --------------------------- Private methods --------------------------- //

    /// <summary>
        /// Set the object state 'grabbed' to true.
    /// </summary>
    private void objectGrabbed() {
        _grabbed = true;
    }

    /// <summary>
        /// Set the object state 'grabbed' to false.
    /// </summary>
    private void objectNotGrabbed() {
        _grabbed = false;
    }

    /// <summary>
        /// Gets the material that corresponds to the object's name.
    /// </summary>
    /// <param name="name"> The name of the object to look for. </param>
    /// <param name="highlight"> Whether the material should be highlighted or not. </param>
    /// <returns> The material that corresponds to the object's name. </returns>
    private Material GetMaterial(string name, bool highlight) {
        foreach (Materials objMat in materials) {
            if (objMat.name == name) {
                return highlight ? objMat.highlightMaterial : objMat.defaultMaterial;
            }
        }
        return null;
    }

    // ----------------------------- Unity methods ----------------------------- //

    private void Update() {
        if (_objectInVision != null) {
            Renderer selectionRenderer = _objectInVision.GetComponent<Renderer>();
            selectionRenderer.material = GetMaterial(_objectInVision.name, false);
            _objectInVision = null;
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
        if ((selectedRenderer == null) || (_grabbed)) return;

        selectedRenderer.material = GetMaterial(newObjectInVision.name, true);
        if (SceneManager.GetActiveScene().name == "Level_0") OnHighlight();
        _objectInVision = newObjectInVision;
    }

    /// <summary>
        /// Set the functions to be called when an object is grabbed / dropped.
    /// </summary>
    private void Start() {
        _grabbed = false;
        notifier.OnGrab += objectGrabbed;
        notifier.OnDrop += objectNotGrabbed;
    }
}