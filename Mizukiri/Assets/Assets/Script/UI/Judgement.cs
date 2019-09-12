using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Judgement : MonoBehaviour
{
    public RigidController stone_script;

    int counter;
    bool flag;
    int judgement;

    // Use this for initialization
    void Start()
    {
        counter = 0;
        flag = false;

    }

    // Update is called once per frame
    void Update()
    {

        if ((stone_script.GetJUDGEMENT() != 0) && (flag == false)) //現在表示されていなくて　かつ　何か判定をしているなら
        {
            flag = true; //表示フラグ
            judgement = stone_script.GetJUDGEMENT(); //判定を受け取る
        }

        if (flag == true) //判定が成功したなら
        {
            counter++;

            switch (judgement) //判定により処理を変更
            {
                case (int) JUDGEMENT.error:
                    //例外処理
                    this.GetComponent<Text>().text = "橋本へ連絡"; //判定数がおかしくなるバグ検知
                    break;

                case (int) JUDGEMENT.good:
                    //例外処理
                    this.GetComponent<Text>().text = "GOOD!";　 //goodと表示する
                    break;

                case (int) JUDGEMENT.perfect:

                    //perfectの時

                    this.GetComponent<Text>().text = "PERFECT!!"; //perfectと表示する
                    break;

                case (int) JUDGEMENT.miss:
                    //遅すぎたときの処理
                    this.GetComponent<Text>().text = "miss"; //早すぎたときのmiss表示
                    break;

                case (int) JUDGEMENT.bad:
                    //遅すぎたときの処理
                    this.GetComponent<Text>().text = "miss"; //遅すぎたときのmiss表示
                    break;
            }

        }
        else
        {
            this.GetComponent<Text>().text = " "; //判定されてない時は表示しない
        }

        if (counter == 30) //0.5秒だけ表示する
        {
            flag = false; //表示をやめる
            counter = 0; //カウンターを初期化する
        }
    }
}