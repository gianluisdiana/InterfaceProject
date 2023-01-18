using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Vertical : MonoBehaviour {
    private bool goUp;

    // Start is called before the first frame update
    void Start() {
        goUp = true;
    }

    // Update is called once per frame
    void Update() {
        Vector3 currentPosition = transform.position; //Posición a tiempo real
        Vector3 initialPosition = new Vector3(0.8f, 1.35f, 1); // Posición inicial
        Vector3 finalPosition = new Vector3(initialPosition.x, initialPosition.y + 2, initialPosition.z); //Posición final

        if (goUp) {
            if (currentPosition.y < finalPosition.y) {
                transform.Translate(Vector3.up * 0.01f);
            } else {
                goUp = false;
            }
        } else {
            if (currentPosition.y > initialPosition.y) {
                transform.Translate(Vector3.down * 0.01f);
            } else {
                goUp = true;
            }
        }
    }
}