using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidCont : MonoBehaviour
{

    
    private Rigidbody2D rigid2D;    //石のrigidbody


    //石の速度用変数===============================================

    [Range(0, 200)]
    public float right_force;   //右方向への力(インスペクターで設定)
    [Range(0, 20)]
    public float up_force;     //上方向への力(インスペクターで設定)

    //=============================================================


    //石の加速用変数===============================================

    private int counter;    //反射した回数を記憶する変数    
    [Range(0, 10)]
    public int count_Ev;    //反射するたびに加速するための係数
    [Range(0, 20)]
    public int counter_limit;   //加速上限

    //=============================================================


    //各種カウンターなど===========================================

    private bool start_flag;    //ゲームのシーン管理用変数

    private bool hit_checker;   //当たっているかの判定用変数
    private int hit_counter; //何個のコライダーと当たってるかを判断　判定を行う

    private bool  push_checker;  //ボタンが押されたかの判定用変数


    //=============================================================


    // Use this for initialization
    void Start()
    {
        start_flag = false;    //ゲームが始まってないと設定

        
        this.rigid2D = GetComponent<Rigidbody2D>();
        rigid2D.isKinematic = true;     //始まるまではkinematicにして固定する
        



        //石の主要変数　　コメントアウトしてある変数は inspector上で変更可能
        //※マスター版制作時には数値を入力し変数を private にしておくこと

        //right_force = 50;
        //up_force = 5;

        //速度の加算カウンター
        counter = 0;
        //count_Ev = 5;
        //counter_limit = 10;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (start_flag) //ゲームが始まっているか
        {

            if ((hit_checker == true) && (push_checker == false))  //判定と当たっていて事前にボタンが押されてないか
            {
                if (Input.GetKeyDown(KeyCode.Space))    //ボタンが押されたなら
                {
                    StoneReflect();     //石の反射処理
                }
            }


            if ((Input.GetKeyDown(KeyCode.Space)) && (hit_checker == false))//当たっていない時にSpaceが押されたか
            {
                push_checker = true;    //押されたと判定する
            }
        }

        hit_checker = false;    //hit_checkerを初期化する
        hit_counter = 0;



        GameStart();   //ゲームの開始を確認する処理

    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        hit_checker = true; //当たっている
        hit_counter++;
    }






    //処理関数//__//__//__//__//__//__//__//__//__//__//__//__//__//__//__//__


    //===============================================================================
    //関数名 GameStart
    //内容　 ゲーム開始する前の停止と開始時の速度　
    //===============================================================================
    void GameStart()
    {
        if (start_flag == false)    //スタートしていないとき
        {
            if (Input.GetKeyDown(KeyCode.Space))    //スペースキーが押されたら
            {
                start_flag = true;  //ゲーム開始
                rigid2D.isKinematic = false;    //石の固定を解除

                //石に速度を加える
                this.rigid2D.AddForce(transform.right * (right_force + counter * count_Ev), ForceMode2D.Impulse);
            }
        }
    }

    //===============================================================================
    //関数名 StoneReflect
    //内容　 水面での反射(速度計算の処理)　
    //===============================================================================
    void StoneReflect()
    {
        rigid2D.velocity = Vector2.zero;    //前の速度を消す

        if (counter < counter_limit) 　//カウンターが限界値を超えてないなら
        {
            counter++;  //カウンターを進める
        }

        //横方向に速度を加える {右向きの力　* (反射した回数　* 係数)}
        this.rigid2D.AddForce(transform.right * (right_force + counter * count_Ev), ForceMode2D.Impulse);

        //縦方向に速度を加える
        this.rigid2D.AddForce(transform.up * up_force, ForceMode2D.Impulse);

        push_checker = false;   //押されたというフラグをoffにする

        if (hit_counter == 2)
        {
            Debug.Log("perfect!");
        }
        if (hit_counter == 1)
        {
            Debug.Log("good!");
        }

    }


    //アクセサ//__//__//__//__//__//__//__//__//__//__//__//__//__//__//__//__

    //===============================================================================
    //関数名 GetStonePos
    //内容　 石の座標を他のオブジェクトに渡す
    //使用　 カメラ　背景スクロール
    //===============================================================================
    public Vector3 GetStonePos()
    {
        return this.transform.position;
    }

    //===============================================================================
    //関数名 GetStatFlag
    //内容　 ゲームがスタートしたかどうかを伝える変数
    //使用　 背景スクロール
    //===============================================================================
    public bool GetStatFlag()
    {
        return start_flag;
    }
}
