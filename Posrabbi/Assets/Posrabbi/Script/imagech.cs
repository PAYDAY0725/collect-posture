using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class imagech : MonoBehaviour
{
    // Image コンポーネントを格納する変数
    private Image m_Image;
    // スプライトオブジェクトを格納する配列
    public Sprite[] m_Sprite;
    // スプライトオブジェクトを変更するための変数
    int a;

    //音声用関数
    //public AudioClip sound1;
    private AudioSource audioSource;

    // ゲーム開始時に実行する処理
    void Start()
    {
        // Image コンポーネントを取得
        m_Image = GetComponent<Image>();

        //audioコンポーネントを取得
        audioSource = GetComponent<AudioSource>();
    }

    // ゲーム実行中に毎フレーム実行する処理
    void Update()
    {
        //gezi.csの変数image_chをaに代入
        a =gezi.image_ch;

        
        // スプライトオブジェクトの変更フラグが true の場合
        if (a == 0)
            {
                // スプライトオブジェクトの変更
                m_Image.sprite = m_Sprite[0];
                
            }
            else if (a == 1)
            {
                //Debug.Log("copy pose is " + a);
                m_Image.sprite = m_Sprite[1];

            }
            // スプライトオブジェクトの変更フラグが false の場合
               else if(a==2)
            {
            
                 // スプライトオブジェクトの変更
                 m_Image.sprite = m_Sprite[2];
                 //("実行する関数",○○秒後に実行)
                 Invoke("sound", 5);
        }
        }
    
    void sound() {
        //GetComponent<AudioSource>().Play();
         audioSource.Play();
    }
    
}



