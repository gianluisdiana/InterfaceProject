using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Follow the basket position
public class LightFollowBasket : MonoBehaviour {
    // ------------------------- Serialized values ------------------------- //

    [Tooltip("The position to be followed")]
    [SerializeField] private Transform basketPos;

    // --------------------------- Unity methods --------------------------- //

    /// <summary>
        /// Keep following the established position
    /// </summary>
    void Update() {
        Vector3 relativePos = basketPos.position - transform.position;
        transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
    }
}
