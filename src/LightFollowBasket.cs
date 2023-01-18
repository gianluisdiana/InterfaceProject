using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFollowBasket : MonoBehaviour
{
    public Transform basketPos;

    void Update() {
        Vector3 relativePos = basketPos.position - transform.position;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;
    }
}
