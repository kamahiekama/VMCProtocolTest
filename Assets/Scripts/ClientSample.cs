using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OscCore;
using System.Text;
using System;

public class ClientSample : MonoBehaviour
{
    public string Host = "127.0.0.1";
    public int Port = 39539;

    private OscClient client;

    // Start is called before the first frame update
    void Start()
    {
        client = new OscClient(Host, Port);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Send");
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
            byte[] baName = Encoding.GetEncoding("UTF-8").GetBytes("root");
            byte[] ba = new byte[baName.Length + 13 * 4];
            for (int i = 0; i < baName.Length; i++)
            {
                ba[i] = baName[i];
            }

            append(ba, baName.Length + 0, p.x);
            append(ba, baName.Length + 4, p.y);
            append(ba, baName.Length + 8, p.z);

            client.Send("/VMC/Ext/Root/Pos", ba, ba.Length);
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
