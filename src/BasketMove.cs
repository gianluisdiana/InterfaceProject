using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketMove : MonoBehaviour {
    private bool goToLeft;

    // Start is called before the first frame update
    void Start() {
        goToLeft = true;
    }

    // Update is called once per frame
    void Update() {
        Vector3 currentPosition = transform.position; //Posición a tiempo real
        Vector3 initialPosition = new Vector3(-3, 0, 2); // Posición inicial
        Vector3 finalPosition = new Vector3(initialPosition.x + 4, initialPosition.y, initialPosition.z); //Posición final

        if (goToLeft) {
            if (currentPosition.x < finalPosition.x) {
                transform.Translate(Vector3.right * 0.003f);
            } else {
                goToLeft = false;
            }
        } else {
            if (currentPosition.x > initialPosition.x) {
                transform.Translate(Vector3.left * 0.003f);
            } else {
                goToLeft = true;
            }
        }
    }
}
