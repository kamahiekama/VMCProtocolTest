using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;
using uOSC;

public class ClientSample : MonoBehaviour
{
    private uOscClient client;

    // Start is called before the first frame update
    void Start()
    {
        client = GetComponent<uOscClient>();
    }

    // Update is called once per frame
    void Update()
    {
//        if (Input.GetKeyDown(KeyCode.Space))
        {
//            Debug.Log("Send");
            Vector3 p = transform.position;
            Quaternion q = transform.rotation;
            Vector3 s = transform.localScale;

            //v2.1
            //  / VMC / Ext / Root / Pos(string){ name}
            //(float){ p.x}
            //(float){ p.y}
            //(float){ p.z}
            //(float){ q.x}
            //(float){ q.y}
            //(float){ q.z}
            //(float){ q.w}
            //(float){ s.x}
            //(float){ s.y}
            //(float){ s.z}
            //(float){ o.x}
            //(float){ o.y}
            //(float){ o.z}
            //            client.Send("/VMC/Ext/Root/Pos", "root", p.x, p.y, p.z, q.x, q.y, q.z, q.w, s.x, s.y, s.z, (float)0, (float)0, (float)0);
            client.Send("/VMC/Ext/Bone/Pos", "Hips", p.x, p.y, p.z, q.x, q.y, q.z, q.w);
            client.Send("/VMC/Ext/Root/Pos", "root", p.x, p.y, p.z, q.x, q.y, q.z, q.w);
            client.Send("/VMC/Ext/OK", 0);
            client.Send("/VMC/Ext/T", Time.realtimeSinceStartup);

        }
    }

    private static void append(byte[] ba, int index, float value)
    {
        byte[] byteArray = BitConverter.GetBytes(value);
        for(int i = 0; i < 4; i++)
        {
            ba[index + i] = byteArray[i];
        }
    }
}
