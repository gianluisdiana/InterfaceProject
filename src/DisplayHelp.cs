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
  private string pickHelp = "Press 'G' to pick up an object";

  /// <value> Keeps track if the pick help was already displayed. </value>
  private bool pickHelpAlreadyDisplayed = false;

  /// <value> String with help on how to throw an object. </value>
  private string throwHelp = "Press 'T' to throw the object.";

  /// <value> String with help on how to drop an object. </value>
  private string dropHelp = "Press 'R' to drop the object.";

  /// <value> Keeps track if the 'drop and throw' help was already displayed.</value>
  private bool dropThrowHelpAlreadyDisplayed = false;

  // -------------------------- Private Methods --------------------------- //

  /// <summary>
    /// Display help the first time the user points to a pick-able object.
  /// </summary>
  IEnumerator displayPickHelp() {
    // Only display it once
    if (this.pickHelpAlreadyDisplayed) yield break;

    this.pickHelpAlreadyDisplayed = true;
    // If there's any text, wait till it disappears
    while (this.commonHelpText.text != "") yield return new WaitForSeconds(0.5f);

    this.commonHelpText.text = this.pickHelp;
    // Keep the text 5 seconds
    yield return new WaitForSeconds(5f);
    this.commonHelpText.text = "";
  }

  /// <summary>
    /// Display help the first time the user is holding an object.
  /// </summary>
  IEnumerator displayDropThrowHelp() {
    // Only display it once
    if (this.dropThrowHelpAlreadyDisplayed) yield break;

    this.dropThrowHelpAlreadyDisplayed = true;
    // If there's any text, wait till it disappears
    while (this.commonHelpText.text != "") yield return new WaitForSeconds(0.5f);

    this.commonHelpText.text = this.dropHelp + "\n" + this.throwHelp;
    // Keep the text 5 seconds
    yield return new WaitForSeconds(5f);
    this.commonHelpText.text = "";
  }

  private void displayPick() {
    StartCoroutine(displayPickHelp());
  }

  private void displayDropThrow() {
    StartCoroutine(displayDropThrowHelp());
  }

  private void Start() {
    this.selectionNotifier.OnHighlight += displayPick;
    this.pickNotifier.OnGrab += displayDropThrow;
  }
}