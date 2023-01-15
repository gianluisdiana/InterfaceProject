using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ReSpawnBox : MonoBehaviour {
    [Tooltip("Where the box will re-spawn.")]
    [SerializeField] private Transform spawnPoint;

    /// <summary>
        /// Re-spawns the box at the given position.
        /// It will also reset its velocity and angle.
    /// </summary>
    private void ReSpawn () {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = spawnPoint.position + new Vector3(0, 0.5f, 0);
        transform.eulerAngles = Vector3.zero;
    }

    /// <summary>
        /// If the box collides with the death zone, re-spawn it.
    /// </summary>
    private void OnCollisionEnter (Collision collision) {
        if (collision.gameObject.tag == "DeathZone") ReSpawn();
    }
}