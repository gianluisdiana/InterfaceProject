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
    public event ButtonPressed OnCirclePressed;
    public event ButtonPressed OnSquarePressed;
    public event ButtonPressed OnXPressed;

    // --------------------------- Unity methods --------------------------- //

    /// <summary>
        /// Checks if any of the ps4 buttons was pressed.
    /// </summary>
    private void Update () {
        if (Input.GetButton("PS4-Crl")) {
            OnCirclePressed();
        } else if (Input.GetButton("PS4-Sqr")) {
            OnSquarePressed();
        } else if (Input.GetButton("PS4-X")) {
            OnXPressed();
        }
    }
}