using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ReSpawnBox : MonoBehaviour {
    // ------------------------- Serialized values ------------------------- //

    [Tooltip("Where the box will re-spawn.")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Light spawnLight;

    /// <summary>
        /// Re-spawns the box at the given position.
        /// It will also reset its velocity and angle.
    /// </summary>
    private void ReSpawn () {
        StartCoroutine(FlashingLight());
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = spawnPoint.position + new Vector3(0, 0.5f, 0);
        transform.eulerAngles = Vector3.zero;
    }

    IEnumerator FlashingLight() {
        spawnLight.enabled = false;
        yield return new WaitForSeconds(0.3f);
        spawnLight.enabled = true;
        yield return new WaitForSeconds(0.1f);
        spawnLight.enabled = false;
        yield return new WaitForSeconds(0.3f);
        spawnLight.enabled = true;
    }

    /// <summary>
        /// If the box collides with the death zone, re-spawn it.
    /// </summary>
    private void OnCollisionEnter (Collision collision) {
        if (collision.gameObject.tag == "DeathZone") ReSpawn();
    }
}