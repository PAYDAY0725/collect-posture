using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class RigControl : MonoBehaviour
{
    
    public GameObject humanoid;
    public Vector3 bodyRotation = new Vector3(0, 0, 0);
    public Vector3 bodyPosition = new Vector3(0,0,0);

    //Boneのデータが入る変数
    RigBone Neck;

    float x;
    
    void Start()
    {
        //変数にボーンのデータを代入
        Neck = new RigBone(humanoid, HumanBodyBones.Head);
    }
    void Update()
    {
        
        //fire.csの変数aをxに代入
        x = gezi.z;
       
        Debug.Log("copy pose is " + x);
        //x = 15;

        //首振り機能
        Neck.set((x), 1, 0, 0);

        humanoid.transform.rotation
          = Quaternion.AngleAxis(bodyRotation.z, new Vector3(0, 0, 1))
          * Quaternion.AngleAxis(bodyRotation.x, new Vector3(1, 0, 0))
          * Quaternion.AngleAxis(bodyRotation.y, new Vector3(0, 1, 0));
    }
}