using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpawnBox : MonoBehaviour {
  [Tooltip("Where the box will re-spawn.")]
  [SerializeField] private Transform spawnPoint;

  /// <summary>
  /// Re-spawns the box at the given position.
  /// If it has a rigidbody, it will also reset its velocity.
  /// </summary>
  private void ReSpawn () {
    if (GetComponent<Rigidbody>() != null) GetComponent<Rigidbody>().velocity = Vector3.zero;
    transform.position = spawnPoint.position + new Vector3(0, 0.5f, 0);
  }

  /// <summary>
  /// If the box collides with the death zone, re-spawn it.
  /// </summary>
  private void OnCollisionEnter (Collision collision) {
    if (collision.gameObject.name == "DeathZone") ReSpawn();
  }
}