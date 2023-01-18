using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private Rigidbody rigid;
    private float distance_plain;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        distance_plain = transform.position.y;
        Debug.Log(transform.position.x + " " + transform.position.z + " " + distance_plain);

         if(distance_plain < 0.3F && transform.position.x > 3.5F && transform.position.x < 4.5F && transform.position.z > 1.5F && transform.position.z < 2.5F)
        {
            rigid.AddForce(Vector3.up * 1F, ForceMode.Impulse);
        }
    }
}
