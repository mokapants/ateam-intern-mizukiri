using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField] Transform stoneTransform; // 石の座標
    [SerializeField] Transform currentBackGround; // 現在のバックグラウンドの親オブジェクト
    [SerializeField] Transform nextBackGround; // 次のバックグラウンドの親オブジェクト

    bool isChangeTime; // バックグラウンドのシフト処理用変数

    static Stage currentStage;
    public static Stage nextStage;

    void Awake()
    {
        isChangeTime = false;

        currentStage = 0;
        nextStage = 0;
    }

    void Update()
    {
        if (stoneTransform.position.x % BackGround.width <= 10)
        {
            if (isChangeTime)
            {
                isChangeTime = false;
                GenerationReplacement(nextStage);
            }
        }
        else
        {
            isChangeTime = true;
        }
    }

    void GenerationReplacement(Stage stage)
    {
        // ステージ遷移
        if (currentStage != stage)
        {
            stage = currentStage + 1;
        }

        // delete all backgrounds of current in directory
        foreach (Transform background in currentBackGround)
        {
            GameObject.Destroy(background.gameObject);
        }

        // generation replacement
        foreach (Transform background in nextBackGround.GetComponentsInChildren<Transform>())
        {
            if (background == nextBackGround)
            {
                continue;
            }
            background.SetParent(currentBackGround, false);
            background.localPosition = new Vector3(0, background.localPosition.y, background.localPosition.z);
        }

        // shift position
        currentBackGround.position += Vector3.right * BackGround.width;
        nextBackGround.position += Vector3.right * BackGround.width;

        // generate new backgrounds
        var near = Resources.Load<BackGroundStageData>("data/graphic/BackGround").backGroundStages[(int) stage].near;
        var middle = Resources.Load<BackGroundStageData>("data/graphic/BackGround").backGroundStages[(int) stage].middle;
        var far = Resources.Load<BackGroundStageData>("data/graphic/BackGround").backGroundStages[(int) stage].far;
        Instantiate(near, nextBackGround);
        Instantiate(middle, nextBackGround);
        Instantiate(far, nextBackGround);

        //generate wave
        var wave = Resources.Load<GameObject>("prefabs/wave/wave");
        Instantiate(wave, nextBackGround);

        currentStage = nextStage;
    }
}