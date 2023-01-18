using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.name != "Box") {
            Destroy(other.gameObject);
        } else {
            GameManager.Instance.NextGameState();
        }
    }
}
