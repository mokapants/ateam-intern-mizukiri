using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//判定用の列挙型
public enum JUDGEMENT
{
    good = 1, //good   
    perfect, //perfect
    miss, //早すぎた
    bad, //遅すぎた

    error //例外処理
}

public class RigidController : MonoBehaviour
{
    private Rigidbody2D stoneRigid2D; //石のrigidbody

    //石の速度用変数===============================================
    [Range(0, 200), SerializeField] float rightForce; //右方向への力(インスペクターで設定)
    //[Range(0, 200)]
    [SerializeField] float upForce; //上方向への力(インスペクターで設定)

    //石の速度変動用の変数===============================================
    private int feverCounter; //feverになるためのカウンター
    [Range(1, 10), SerializeField] int feverRequired; //何回でフィーバーになるか　　　master版制作時は定数で
    [Range(1, 5), SerializeField] float feverAcceleration; //fever時の加速度  (1.5なら1.5倍)     master版制作時は定数で

    private int downCounter; //減速する際のカウンター    
    [Range(0, 100), SerializeField] int downDeceleration; //減速率 (5なら１回あたり5%現象)    master版制作時は定数で
    [Range(0, 20), SerializeField] int downLimit; //減速上限　　　　　master版制作時は定数で

    //各種カウンターなど===========================================
    private bool isStart; //ゲームが始まってればtrue
    private bool isGameOver; // ゲームオーバーならtrue
    private bool isHit; //当たっているかの判定用変数
    private bool[] hitFlag; // 判定にヒットしてるかどうかのフラグ
    private int judgement; //前回の判定を記憶する変数
    private bool isPush; //ボタンが押されたかの判定用変数

    void Start()
    {
        isStart = false;
        isGameOver = false;
        hitFlag = new bool[2] { false, false };

        stoneRigid2D = GetComponent<Rigidbody2D>();

        //各種変数　　コメントアウトしてある変数は inspector上で変更可能
        //マスター制作時に必要なものは定数にすること  

        feverCounter = 0;
        //feverRequired;   
        //feverAcceleration;   

        downCounter = 0;
        downDeceleration /= 100;
        //downDeceleration;
        //downLimit；
    }

    void Update()
    {
        //ゲーム始まるまで待機
        if (!isStart)
        {
            if (Input.GetKeyDown(KeyCode.Space)) //スペースキーが押されたら
            {
                GameStart(); //ゲームの開始を確認する処理
            }
            return;
        }

        if (isGameOver)
        {
            // ゲームオーバー用の処理

            return;
        }

        judgement = 0;

        if (Input.GetKeyDown(KeyCode.Space)) //ボタンが押されたなら
        {
            if (!isHit) //判定と当たっていない時にSpaceが押されたか
            {
                isPush = true;
                isGameOver = true;
                judgement = (int) JUDGEMENT.miss; //miss判定を行う
            }

            if (isHit && !isPush) //判定と当たっていて事前にボタンが押されてないか
            {
                StoneReflect(); //石の反射処理
            }
        }
    }

    private void LateUpdate()
    {
        if (!hitFlag[0] && !hitFlag[1])
        {
            isHit = false; //isHitを初期化する
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        isHit = true; //当たっている

        string tag = other.gameObject.tag;
        if (tag == "Perfect")
        {
            hitFlag[0] = true;
        }
        else if (tag == "Good")
        {
            hitFlag[1] = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        string tag = other.gameObject.tag;
        if (tag == "Perfect")
        {
            hitFlag[0] = false;
        }
        else if (tag == "Good")
        {
            hitFlag[1] = false;
        }
    }

    //処理関数//__//__//__//__//__//__//__//__//__//__//__//__//__//__//__//__
    /// <summary>
    /// ゲーム開始する前の停止と開始時の速度
    /// </summary>
    void GameStart()
    {
        isStart = true; //ゲーム開始
        stoneRigid2D.isKinematic = false; //石の固定を解除

        //石に速度を加える
        stoneRigid2D.AddForce(transform.right * rightForce, ForceMode2D.Impulse);
    }

    /// <summary>
    /// 水面での反射(速度計算の処理)
    /// </summary>
    void StoneReflect()
    {
        stoneRigid2D.velocity = Vector2.zero; //前の速度を消す

        //横方向に速度を加える {右向きの力　* (反射した回数　* 係数)}
        stoneRigid2D.AddForce(transform.right * rightForce, ForceMode2D.Impulse);

        //縦方向に速度を加える
        stoneRigid2D.AddForce(transform.up * (upForce /*- counter/3*/ ), ForceMode2D.Impulse);

        if (hitFlag[0])
        {
            //perfect時の処理を書く
            if (feverCounter < feverRequired) //
            {
                feverCounter++; //カウンターを進める
            }
            else
            {
                rightForce = rightForce * feverAcceleration;

                ////upForce = 5.5f;
                feverCounter = 0;
                downCounter = 0;

            }

            judgement = (int) JUDGEMENT.perfect; //perfect判定を出す
        }
        else if (hitFlag[1])
        {
            //good時の処理を書く
            judgement = (int) JUDGEMENT.good; //good判定を出す

            if (downCounter < downLimit)
            {
                downCounter--;
            }
        }
        else
        {
            judgement = (int) JUDGEMENT.error; //エラーログ
        }

        isPush = false; //押されたというフラグをoffにする
    }

    //アクセサ//__//__//__//__//__//__//__//__//__//__//__//__//__//__//__//__
    /// <summary>
    /// 石の座標を他のオブジェクトに渡す関数
    /// 使用　 カメラ　背景スクロール
    /// </summary>
    /// <returns>石の座標</returns>
    public Vector2 GetStonePos()
    {
        return this.transform.position;
    }

    /// <summary>
    /// プレイヤーの速度を渡す関数 
    /// 使用　 背景スクロールに渡してスクロール速度を変更
    /// </summary>
    /// <returns>プレイヤーの速度</returns>
    public float GetStoneSpeed()
    {
        return rightForce;
    }

    /// <summary>
    /// ゲームがスタートしたかどうかを伝える関数
    /// 使用　 背景スクロール
    /// </summary>
    /// <returns>isStart</returns>
    public bool GetStatFlag()
    {
        return isStart;
    }

    /// <summary>
    /// perfectかGoodかをUIに渡す関数
    /// 使用　 UIによる表示やエフェクト発生フラグ
    /// </summary>
    /// <returns>ジャッジ結果</returns>
    public int GetJudgement()
    {
        return judgement;
    }

}