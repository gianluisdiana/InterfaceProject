using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Detect when the user scores
public class WinDetection : MonoBehaviour {
    /// <summary>
        /// When the box collides with the scoreTarget object, go to the next level.
    /// </summary>
    /// <param name="other"> Object that collides with. Probably the box. </param>
    private void OnTriggerEnter(Collider other) {
        if (other.name != "Box") {
            Destroy(other.gameObject);
        } else {
            GameManager.Instance.NextGameState();
        }
    }
}
