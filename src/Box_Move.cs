using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Move : MonoBehaviour
{
    // Start is called before the first frame update
    private bool goToLeft;
    private bool goUp;
    void Start()
    {
        goToLeft = true;
        goUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position; //Posición a tiempo real
        Vector3 initialPosition = new Vector3(0.8f, 1.35f, 1); // Posición inicial
        Vector3 leftPosition = new Vector3(initialPosition.x - 2, initialPosition.y, initialPosition.z); //Posición izquierda
        Vector3 upperPosition = new Vector3(initialPosition.x, initialPosition.y + 2, initialPosition.z); //Posición encima
        Vector3 rightPosition = new Vector3(initialPosition.x + 2, initialPosition.y, initialPosition.z); //Posición derecha

        if (goToLeft && goUp) {
            Debug.Log("goleft");
            if (currentPosition.x > leftPosition.x) {
                transform.Translate(Vector3.left * 0.02f);
            } else {
                Debug.Log("!goToLeft");
                goToLeft = false;
            }
        } else if (goUp){
            Debug.Log("goup");
            if (currentPosition.y < upperPosition.y) {
                transform.Translate(Vector3.up * 0.02f);
            } else {
                Debug.Log("!goToup");
                goUp = false;
            }
        } else if (!goToLeft){
            Debug.Log("goright");
            if (currentPosition.x < initialPosition.x) {
                transform.Translate(Vector3.right * 0.02f);
            } else {
                Debug.Log("goToLeft");
                goToLeft = true;
            }
        } else if (!goUp){
            Debug.Log("godown");
            if (currentPosition.y > initialPosition.y) {
                transform.Translate(Vector3.down * 0.02f);
            } else {
                Debug.Log("goToup");
                goUp = true;
            }
        }
         
        /* if(currentPosition == initialPosition){
            goToLeft = true;
            goUp = true;
        } */
    }
}
