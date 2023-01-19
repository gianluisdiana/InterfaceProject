using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {
    /// <value> Represents the GameManager instance to be used by other scripts </value>
    public static GameManager Instance;

    // ----------------------------- Unity methods ----------------------------- //

    /// <summary>
        /// Initialize the Instance static object.
    /// </summary>
    void Awake() {
        Instance = this;
    }

    /// <summary>
        /// Load the next scene (level).
        /// If the current scene is the last one, come back to first one.
    /// </summary>
    public void NextGameState() {
        if (SceneManager.GetActiveScene().buildIndex == (SceneManager.sceneCountInBuildSettings - 1)) {
            SceneManager.LoadScene(0);
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}