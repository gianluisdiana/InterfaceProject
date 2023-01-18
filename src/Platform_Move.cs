using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Move : MonoBehaviour
{
    private bool goForward;
    private bool goLeft;
    private bool finalPos;

    private GameObject box;

    public Transform parent;

    public Transform oldParent;

    private Vector3 initialPosition = new Vector3(-1, 1.2F, -2.75F); // Posición inicial

    // Start is called before the first frame update
    void Start()
    {
        goForward = true;
        finalPos = false;
        goLeft = false;
        box = GameObject.FindWithTag("canPickUp");
        
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == box.tag) {
            box.transform.SetParent(parent);
            Debug.Log("Colision caja");
            move();
        }
    }

    void OnCollisionExit()
    {
        box.transform.SetParent(oldParent);
        transform.position = initialPosition;
        goForward = true;
        goLeft = false;
    }

    void move()
    {
        Vector3 currentPosition = transform.position; //Posición a tiempo real
        Vector3 frontPosition = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z + 5.5F); //Posición al frente
        Vector3 finalPosition = new Vector3(initialPosition.x-2, initialPosition.y, initialPosition.z); //Posición al frente

        if(!finalPos){
            if (goForward) {
                if (currentPosition.z < frontPosition.z) {
                    transform.Translate(Vector3.forward * 0.01f);
                } else {
                    goForward = false;
                    goLeft = true;
                }
            } else if (goLeft) {
                if (currentPosition.x > finalPosition.x) {
                    transform.Translate(Vector3.left * 0.01f);
                } else {
                    goLeft = false;
                }
            } else {
                transform.Rotate(0, 0, 90);
                finalPos = true;
            }
        }
    }
}
