using System.Collections;
using System.Collections.Generic; // Dictionary
using UnityEngine;
using TMPro; // TMP_Text

[RequireComponent(typeof (PickUpScript))]
public class DisplayHelp : MonoBehaviour {
  // ------------------------- Serialized Values -------------------------- //

  [Tooltip("The text object where will be displayed the help")]
  [SerializeField] private TMP_Text commonHelpText;

  [Tooltip("The script that will notify when an object is grabbed.")]
  [SerializeField] private PickUpScript pickNotifier;

  [Tooltip("The script that will notify when an object is highlighted")]
  [SerializeField] private SelectionManager selectionNotifier;

  // ------------------------- Private attributes ------------------------- //

  /// <value> String with help on how to grab an object. </value>
  private string _pickHelp = "Press 'O' to pick up an object";

  /// <value> Keeps track if the pick help was already displayed. </value>
  private bool _pickHelpAlreadyDisplayed = false;

  /// <value> String with help on how to throw an object. </value>
  private string _throwHelp = "Press 'X' to throw the object.";

  /// <value> String with help on how to drop an object. </value>
  private string _dropHelp = "Press '■' to drop the object.";

  /// <value> Keeps track if the 'drop and throw' help was already displayed.</value>
  private bool _dropThrowHelpAlreadyDisplayed = false;

  // -------------------------- Private Methods --------------------------- //

  /// <summary>
    /// Display help the first time the user points to a pick-able object.
  /// </summary>
  IEnumerator displayPickHelp() {
    // Only display it once
    if (_pickHelpAlreadyDisplayed) yield break;

    _pickHelpAlreadyDisplayed = true;
    // If there's any text, wait till it disappears
    while (commonHelpText.text != "") yield return new WaitForSeconds(0.5f);

    commonHelpText.text = _pickHelp;
    // Keep the text 5 seconds
    yield return new WaitForSeconds(5f);
    commonHelpText.text = "";
  }

  /// <summary>
    /// Display help the first time the user is holding an object.
  /// </summary>
  IEnumerator displayDropThrowHelp() {
    // Only display it once
    if (_dropThrowHelpAlreadyDisplayed) yield break;

    _dropThrowHelpAlreadyDisplayed = true;
    // If there's any text, wait till it disappears
    while (commonHelpText.text != "") yield return new WaitForSeconds(0.5f);

    commonHelpText.text = _dropHelp + "\n" + _throwHelp;
    // Keep the text 5 seconds
    yield return new WaitForSeconds(5f);
    commonHelpText.text = "";
  }

  private void displayPick() {
    StartCoroutine(displayPickHelp());
  }

  private void displayDropThrow() {
    StartCoroutine(displayDropThrowHelp());
  }

  // ----------------------------- Unity methods ----------------------------- //

  /// <summary>
    /// Set the functions to be called when an object is grabbed / highlight.
  /// </summary>
  private void Start() {
    selectionNotifier.OnHighlight += displayPick;
    pickNotifier.OnGrab += displayDropThrow;
  }
}