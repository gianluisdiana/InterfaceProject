// TODO: NOT TESTED YET, PROBABLY WILL NEED TO BE CHANGED

using System.Collections;
using System.Collections.Generic;
using TextSpeech;
using UnityEngine;
using UnityEngine.Android;
using TMPro;

class VoiceController : MonoBehaviour {

  [Tooltip("The text object to display the data.")]
  [SerializeField] private TMP_Text text;

  /// <value>Language code for the voice recognition</value>
  private const string LANG_CODE = "en-US";

# region Text to Speech

  /// <summary>
  /// Callback the text to speech function to start the speech
  /// </summary>
  public void StartSpeaking (string text) {
    TextToSpeech.Instance.StartSpeak(text);
  }

  /// <summary>
  /// Callback the text to speech function to stop the speech
  /// </summary>
  public void StopSpeaking () {
    TextToSpeech.Instance.StopSpeak();
  }

  void OnSpeakStart () {
    Debug.Log("Talking started...");
  }

  void OnSpeakStop () {
    Debug.Log("Talking stop");
  }

# endregion

# region Speech to Text

  /// <summary>
  /// Callback the speech to text function to start the voice recognition
  /// </summary>
  public void StartListening () {
    SpeechToText.Instance.StartRecording();
  }

  /// <summary>
  /// Callback the speech to text function to stop the voice recognition
  /// </summary>
  public void StopListening () {
    SpeechToText.Instance.StopRecording();
  }

  /// <summary>
  void OnFinalSpeechResult (string result) {
    text.text = "Final result: " + result;
  }

  void OnPartialSpeechResult (string result) {
    text.text = "Partial result: " + result;
  }

# endregion

  /// <summary>
  /// Set up the voice recognition language
  /// </summary>
  private void SetUp (string code) {
    TextToSpeech.Instance.Setting(code, 1, 1);
    SpeechToText.Instance.Setting(code);
  }

  /// <summary>
  /// Check if the app has the permission to use the microphone.
  /// Only will ask if the device is Android.
  /// </summary>
  void CheckPermission () {
    # if UNITY_ANDROID
      if (!Permission.HasUserAuthorizedPermission(Permission.Microphone)) {
        Permission.RequestUserPermission(Permission.Microphone);
      }
    # endif
  }

  void Start () {
    SetUp(LANG_CODE);

    # if UNITY_ANDROID
      SpeechToText.Instance.onPartialResultsCallback = OnPartialSpeechResult;
    # endif
    SpeechToText.Instance.onResultCallback = OnFinalSpeechResult;
    TextToSpeech.Instance.onStartCallBack = OnSpeakStart;
    TextToSpeech.Instance.onDoneCallback = OnSpeakStop;

    CheckPermission();
  }
}