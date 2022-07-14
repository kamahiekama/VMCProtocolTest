using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OscCore;

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
            Vector3 s = transform.scale;
            client.Send("/VMC/Ext/Root/Pos", "root", p.x, p.y, p.z, q.x, q.y, q.z, q.w);
        }
    }
}
