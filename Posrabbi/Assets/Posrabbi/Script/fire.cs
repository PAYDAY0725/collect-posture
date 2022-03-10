using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;



public class fire : MonoBehaviour
{
    //RigControl.csに値を渡す変数
    public static float a;
    //firebaseの値を受け取る変数
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

              //firebaseのValue=の部分を抜き出す
              b=snapshot.Value;
              Debug.Log("Value is " + b);

              //object型をfloat型に変換する
              a = System.Convert.ToSingle(b);
              
              //値が-100以下の場合-100に固定する
              c = (int)a;
              if ( c< -200)
              {
                  c = -200;
              }
              //値が0以上の場合0に固定する
              else if (c >= 0)
              {
                  c = 0;
              }
              Debug.Log("Value is " + c);

          }
      });
    }

}
