/** First of all, allow the microphone to be used in the Unity Editor. */

using System.Collections;
using System.Collections.Generic;
using System.Linq; // ToArray()
using UnityEngine;
using UnityEngine.Windows.Speech; // KeywordRecognizer

public class VoiceRecognition : MonoBehaviour {
  /// <value> Used to recognize the words that are spoken</value>
  private KeywordRecognizer keywordRecognizer;

  /// <value> The words that will be recognized and it response.</value>
  private Dictionary<string, System.Action> actions = new Dictionary<string, System.Action>();

  /// <value> The RigidBody component of the GameObject.</value>
  private Rigidbody rb;

  /// <value> The strength of the jump.</value>
  public float jumpStrength = 5f;

  /// <summary>
  /// Adds a force in the 'Y' axis to make it jump.
  /// </summary>
  void Jump() {
		rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
	}

  /// <summary>
  /// Called when a word is recognized.
  /// </summary>
  /// <param name="speech">The word that was recognized.</param>
  private void WordRecognized(PhraseRecognizedEventArgs speech) {
    Debug.Log(speech.text);
    this.actions[speech.text].Invoke();
  }

  /// <summary>
  /// Add the words that will be recognized and it response.
  /// </summary>
  private void SetUpRecognizer() {
    this.actions.Add("jump", Jump);
    // Create the keyword recognizer with the words established
    this.keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());

    // Register a callback for the KeywordRecognizer and start recognizing
    this.keywordRecognizer.OnPhraseRecognized += this.WordRecognized;
    this.keywordRecognizer.Start();
  }

  private void Start () {
    this.rb = GetComponent<Rigidbody>();
    this.SetUpRecognizer();
  }
}