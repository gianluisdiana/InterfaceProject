using UnityEngine;

[System.Serializable] public struct Materials {
    public string name;
    public Material defaultMaterial;
    public Material highlightMaterial;
}

public class SelectionManager : MonoBehaviour {

    [SerializeField] private Materials[] materials;
    [SerializeField] private PickUpScript notifier;

    private bool _grabed;
    private Transform _selection;

    private void Start() {
        _grabed = false;
        notifier.OnGrab += objectGrabed;
        notifier.OnNotGrab += objectNotGrabed;
    }

    private void Update() {
        if (_selection != null) {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            foreach (Materials objMat in materials) {
                if (objMat.name == _selection.name) {
                    selectionRenderer.material = objMat.defaultMaterial;
                    break;
                }
            }
            _selection = null;
        }

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, notifier.pickUpRange)) {
            var selection = hit.transform;
            if (selection.CompareTag("canPickUp")) {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if ((selectionRenderer != null) && (!_grabed)) {
                    foreach (Materials objMat in materials) {
                        if (objMat.name == selection.name) {
                            selectionRenderer.material = objMat.highlightMaterial;
                            break;
                        }
                    }
                }

                _selection = selection;
            }
        }
    }

    private void objectGrabed() {
        _grabed = true;
    }

    private void objectNotGrabed() {
        _grabed = false;
    }
}