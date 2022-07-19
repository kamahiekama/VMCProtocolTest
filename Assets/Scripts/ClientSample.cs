using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;
using uOSC;

public class ClientSample : MonoBehaviour
{
    private static HumanBodyBones[] bones =
    {
        HumanBodyBones.Hips,
        HumanBodyBones.LeftUpperLeg,
        HumanBodyBones.RightUpperLeg,
        HumanBodyBones.LeftLowerLeg,
        HumanBodyBones.RightLowerLeg,
        HumanBodyBones.LeftFoot,
        HumanBodyBones.RightFoot,
        HumanBodyBones.Spine,
        HumanBodyBones.Chest,
        HumanBodyBones.Neck,
        HumanBodyBones.Head,
        HumanBodyBones.LeftShoulder,
        HumanBodyBones.RightShoulder,
        HumanBodyBones.LeftUpperArm,
        HumanBodyBones.RightUpperArm,
        HumanBodyBones.LeftLowerArm,
        HumanBodyBones.RightLowerArm,
        HumanBodyBones.LeftHand,
        HumanBodyBones.RightHand,
        HumanBodyBones.LeftToes,
        HumanBodyBones.RightToes,
        HumanBodyBones.LeftEye,
        HumanBodyBones.RightEye,
        HumanBodyBones.LeftThumbProximal,
        HumanBodyBones.LeftThumbIntermediate,
        HumanBodyBones.LeftThumbDistal,
        HumanBodyBones.LeftIndexProximal,
        HumanBodyBones.LeftIndexIntermediate,
        HumanBodyBones.LeftIndexDistal,
        HumanBodyBones.LeftMiddleProximal,
        HumanBodyBones.LeftMiddleIntermediate,
        HumanBodyBones.LeftMiddleDistal,
        HumanBodyBones.LeftRingProximal,
        HumanBodyBones.LeftRingIntermediate,
        HumanBodyBones.LeftRingDistal,
        HumanBodyBones.LeftLittleProximal,
        HumanBodyBones.LeftLittleIntermediate,
        HumanBodyBones.LeftLittleDistal,
        HumanBodyBones.RightThumbProximal,
        HumanBodyBones.RightThumbIntermediate,
        HumanBodyBones.RightThumbDistal,
        HumanBodyBones.RightIndexProximal,
        HumanBodyBones.RightIndexIntermediate,
        HumanBodyBones.RightIndexDistal,
        HumanBodyBones.RightMiddleProximal,
        HumanBodyBones.RightMiddleIntermediate,
        HumanBodyBones.RightMiddleDistal,
        HumanBodyBones.RightRingProximal,
        HumanBodyBones.RightRingIntermediate,
        HumanBodyBones.RightRingDistal,
        HumanBodyBones.RightLittleProximal,
        HumanBodyBones.RightLittleIntermediate,
        HumanBodyBones.RightLittleDistal,
    };

    public Animator anim;
    public Transform vrmRoot;

    private uOscClient client;

    // Start is called before the first frame update
    void Start()
    {
        client = GetComponent<uOscClient>();
    }

    // Update is called once per frame
    void Update()
    {
        // camera
        // カメラが必要かは受け取るアプリ次第。とりあえず無くて良い。
        /*
        {
            Transform t = Camera.main.transform;
            Vector3 p = t.localPosition;
            Quaternion q = t.localRotation;
            client.Send("/VMC/Ext/Cam", "main", p.x, p.y, p.z, q.x, q.y, q.z, q.w, Camera.main.fieldOfView);
        }
        //*/

        // blendshape value
        // /VMC/Ext/Blend/Val
        // 口の動きなど。必要。

        // bone pos
        foreach (HumanBodyBones bone in bones)
        {
            Debug.Log(bone);
            Transform t = anim.GetBoneTransform(bone);
            Vector3 p = t.localPosition;
            Quaternion q = t.localRotation;
            client.Send("/VMC/Ext/Bone/Pos", "" + bone, p.x, p.y, p.z, q.x, q.y, q.z, q.w);
        }

        // root
        {
            Transform t = vrmRoot;
            Vector3 p = t.position;
            Quaternion q = t.rotation;
            Vector3 s = t.lossyScale;
            client.Send("/VMC/Ext/Root/Pos", "root", p.x, p.y, p.z, q.x, q.y, q.z, q.w);
            // VSee が対応していない？
            //client.Send("/VMC/Ext/Root/Pos", "root", p.x, p.y, p.z, q.x, q.y, q.z, q.w, s.x, s.y, s.z, (float)0, (float)0, (float)0);
        }
        client.Send("/VMC/Ext/OK", 0);
        client.Send("/VMC/Ext/T", Time.realtimeSinceStartup);
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
