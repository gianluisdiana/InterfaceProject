/** First of all, allow the microphone to be used in the Unity Editor. */

using System.Collections;
using System.Collections.Generic;
using System.Linq; // ToArray()
using UnityEngine;
using UnityEngine.Windows.Speech; // KeywordRecognizer

public class VoiceRecognition : MonoBehaviour {
  // ------------------------------ Notifier ------------------------------ //

  /// <summary>
  /// Sends a regular message to the subscribers.
  /// </summary>
  public delegate void WordSaid();

  /// <summary>
  /// Events that will be triggered when a certain word was said.
  /// </summary>
  public event WordSaid OnJumpSaid;
  public event WordSaid OnGrabSaid;
  public event WordSaid OnThrowSaid;
  public event WordSaid OnDropSaid;


  // -------------------------- Voice Recognizer -------------------------- //

  /// <value> Used to recognize the words that are spoken</value>
  private KeywordRecognizer keywordRecognizer;

  /// <value> The words that will be recognized and it response.</value>
  private Dictionary<string, System.Action> actions = new Dictionary<string, System.Action>();


  // ------------------------------ Methods ------------------------------ //

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
    this.actions.Add("jump", delegate () { OnJumpSaid(); });
    this.actions.Add("grab", delegate () { OnGrabSaid(); });
    this.actions.Add("throw", delegate () { OnThrowSaid(); });
    this.actions.Add("drop", delegate () { OnDropSaid(); });

    // Create the keyword recognizer with the words established
    this.keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());

    // Register a callback for the KeywordRecognizer and start recognizing
    this.keywordRecognizer.OnPhraseRecognized += this.WordRecognized;
    this.keywordRecognizer.Start();
  }

  private void Start () {
    this.SetUpRecognizer();
  }
}