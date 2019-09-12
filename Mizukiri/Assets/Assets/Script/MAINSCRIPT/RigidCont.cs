using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidCont : MonoBehaviour
{
    //判定用の列挙型
    private enum　JUGEMENT
    {
        good = 1,   //good   
        perfect,    //perfect
        miss,       //早すぎた
        miss2,      //遅すぎた


        error       //例外処理
  
    }

    private Rigidbody2D rigid2D;    //石のrigidbody

    
    //石の速度用変数===============================================

    [Range(0, 200)]
    public float right_force;   //右方向への力(インスペクターで設定)
    //[Range(0, 200)]
    public float up_force;     //上方向への力(インスペクターで設定)

    //=============================================================


    //石の速度変動用の変数===============================================
    private int fever_counter;    //feverになるためのカウンター
    [Range(1, 10)]
    public int fever_required;   //何回でフィーバーになるか　　　master版制作時は定数で
    [Range(1, 5)]
    public float fever_ev;   //fever時の加速度  (1.5なら1.5倍)     master版制作時は定数で
    

    private int down_counter;    //減速する際のカウンター    
    [Range(0, 100)]
    public int down_ev;    //減速率 (5なら１回あたり5%現象)    master版制作時は定数で
    [Range(0, 20)]
    public int down_limit;   //減速上限　　　　　master版制作時は定数で

    //=============================================================


    //各種カウンターなど===========================================

    private bool start_flag;    //ゲームのシーン管理用変数

    private bool hit_checker;   //当たっているかの判定用変数
    private int hit_counter;   //当たっている数を判断

    private int judgement; //前回の判定を記憶する変数

    private bool push_checker;  //ボタンが押されたかの判定用変数
    //=============================================================


    // Use this for initialization
    void Start()
    {
        start_flag = false;    //ゲームが始まってないと設定


        this.rigid2D = GetComponent<Rigidbody2D>();
        rigid2D.isKinematic = true;     //始まるまではkinematicにして固定する        



        //各種変数　　コメントアウトしてある変数は inspector上で変更可能
        //マスター制作時に必要なものは定数にすること  

        fever_counter = 0;    //feverになるためのカウンター
        //fever_required;   
        //fever_ev;   

        down_counter = 0;    //減速する際のカウンター    
        down_ev = down_ev / 100;                     //down_ev;    
                             //down_limit；
    }

    // Update is called once per frame
    void Update()
    {

        if (start_flag) //ゲームが始まっているか
        {
            judgement = 0;
           
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
                judgement = (int)JUGEMENT.miss; //miss判定を行う
            }
        }
        hit_counter = 0;        //hit_counterを初期化する

        hit_checker = false;    //hit_checkerを初期化する
    }

    //Update関数終了時に初期化などを行う
    private void LateUpdate()
    {
        hit_counter = 0;        //hit_counterを初期化する

        hit_checker = false;    //hit_checkerを初期化する
        GameStart();   //ゲームの開始を確認する処理
    }

    //コライダーと当たった時の判定処理
    void OnTriggerEnter2D(Collider2D collision)
    {
        hit_checker = true; //当たっている
        hit_counter++;        //hit_counterを進める
        Debug.Log(hit_counter); 
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
                this.rigid2D.AddForce(transform.right * right_force, ForceMode2D.Impulse);
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

        //横方向に速度を加える {右向きの力　* (反射した回数　* 係数)}
        this.rigid2D.AddForce(transform.right * right_force, ForceMode2D.Impulse);

        //縦方向に速度を加える
        this.rigid2D.AddForce(transform.up * (up_force /*- counter/3*/) , ForceMode2D.Impulse);

       
        
        switch (hit_counter)  //判定を行う
        {
            case 0:
                judgement = (int)JUGEMENT.error;   //エラーログ
                break;

            case (int)JUGEMENT.good:

                //good時の処理を書く
                judgement = (int)JUGEMENT.good; //good判定を出す

                if (down_counter < down_limit)
                {
                    down_counter--;
                }

                break;

            case (int)JUGEMENT.perfect:

                //perfect時の処理を書く
                if (fever_counter < fever_required)  //
                {
                    fever_counter++;  //カウンターを進める
                }
                else
                {
                    right_force = right_force * fever_ev;

                    ////up_force = 5.5f;
                    fever_counter = 0;
                    down_counter = 0;

                }

                judgement = (int)JUGEMENT.perfect; //perfect判定を出す

                break;
        }

        push_checker = false;   //押されたというフラグをoffにする
    }


    //アクセサ//__//__//__//__//__//__//__//__//__//__//__//__//__//__//__//__

    //===============================================================================
    //関数名 GetStonePos
    //内容　 石の座標を他のオブジェクトに渡す関数
    //使用　 カメラ　背景スクロール
    //===============================================================================
    public Vector3 GetStonePos()
    {
        return this.transform.position; 
    }

    //===============================================================================
    //関数名 GetStoneSpeed
    //内容　 プレイヤーの速度を渡す関数 
    //使用　 背景スクロールに渡してスクロール速度を変更
    //===============================================================================
    public float GetStoneSpeed()
    {
        return right_force;
    }

    //===============================================================================
    //関数名 GetStatFlag
    //内容　 ゲームがスタートしたかどうかを伝える関数
    //使用　 背景スクロール
    //===============================================================================
    public bool GetStatFlag()
    {
        return start_flag;
    }


    //===============================================================================
    //関数名 GetJudgement
    //内容　 perfectかGoodかをUIに渡す関数
    //使用　 UIによる表示やエフェクト発生フラグ
    //===============================================================================
    public int GetJugement()
    {
        return judgement;
    }

}
