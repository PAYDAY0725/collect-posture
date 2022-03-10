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

    //Bone�̃f�[�^������ϐ�
    RigBone Neck;

    float x;
    
    void Start()
    {
        //�ϐ��Ƀ{�[���̃f�[�^����
        Neck = new RigBone(humanoid, HumanBodyBones.Head);
    }
    void Update()
    {
        
        //fire.cs�̕ϐ�a��x�ɑ��
        x = gezi.z;
       
        Debug.Log("copy pose is " + x);
        //x = 15;

        //��U��@�\
        Neck.set((x), 1, 0, 0);

        humanoid.transform.rotation
          = Quaternion.AngleAxis(bodyRotation.z, new Vector3(0, 0, 1))
          * Quaternion.AngleAxis(bodyRotation.x, new Vector3(1, 0, 0))
          * Quaternion.AngleAxis(bodyRotation.y, new Vector3(0, 1, 0));
    }
}