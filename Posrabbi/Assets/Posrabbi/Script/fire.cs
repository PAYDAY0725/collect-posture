using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;



public class fire : MonoBehaviour
{
    //RigControl.cs�ɒl��n���ϐ�
    public static float a;
    //firebase�̒l���󂯎��ϐ�
    object b;
    public static int c;
    void Update()
    {
        // Get the root reference location of the database.
        //https://firebase.google.com/docs/database/unity/retrieve-data?hl=ja
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        FirebaseDatabase.DefaultInstance
      .GetReference("z")
      .GetValueAsync().ContinueWithOnMainThread(task => {
          if (task.IsFaulted)
          {
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result ;
              // Do something with snapshot...
              //Debug.Log("pose is " + snapshot);

              //firebase��Value=�̕����𔲂��o��
              b=snapshot.Value;
              Debug.Log("Value is " + b);

              //object�^��float�^�ɕϊ�����
              a = System.Convert.ToSingle(b);
              
              //�l��-100�ȉ��̏ꍇ-100�ɌŒ肷��
              c = (int)a;
              if ( c< -200)
              {
                  c = -200;
              }
              //�l��0�ȏ�̏ꍇ0�ɌŒ肷��
              else if (c >= 0)
              {
                  c = 0;
              }
              Debug.Log("Value is " + c);

          }
      });
    }

}
