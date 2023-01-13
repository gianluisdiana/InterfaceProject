using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basket : MonoBehaviour
{
    private bool izq;
    // Start is called before the first frame update
    void Start()
    {
        izq = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 pos_ini = new Vector3(-2, 0, 2);
        Vector3 puntoB = new Vector3(pos_ini.x + 2, pos_ini.y, pos_ini.z);
        Vector3 puntoA = new Vector3(pos_ini.x - 2, pos_ini.y, pos_ini.z);

        if (izq)
        {
            if (pos.x < puntoB.x)
            {
                transform.Translate(Vector3.right * 0.001f);
            }
            else
            {
                izq = false;
            }

        }
        else
        {
            if (pos.x > puntoA.x)
            {
                transform.Translate(Vector3.left * 0.001f);
            }
            else
            {
                izq = true;
            }
        }

    }
}
