using UnityEngine;

public class Interactions : MonoBehaviour {
    // ------------------------------ Notifier ------------------------------ //

    /// <summary>
        /// Sends a regular message to the subscribers when presses a certain button.
    /// </summary>
    public delegate void ButtonPressed();

    /// <summary>
        /// Events that will be triggered when a certain button was pressed.
    /// </summary>
    public event ButtonPressed OnGPressed;
    public event ButtonPressed OnDPressed;
    public event ButtonPressed OnTPressed;
    public event ButtonPressed OnSpaceBarPressed;

    // --------------------------- Unity methods --------------------------- //

    /// <summary>
        /// Checks if any of the buttons was pressed.
    /// </summary>
    private void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            OnSpaceBarPressed();
        } else if (Input.GetKeyDown(KeyCode.G)) {
            OnGPressed();
        } else if (Input.GetKeyDown(KeyCode.D)) {
            OnDPressed();
        } else if (Input.GetKeyDown(KeyCode.T)) {
            OnTPressed();
        }
    }
}