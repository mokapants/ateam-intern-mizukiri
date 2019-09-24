using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstAndOnGameManager : MonoBehaviour
{
    [SerializeField] Text highscoreText;

    void Start()
    {
        highscoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString() + " m";
    }

    void Update()
    {
        if (BackGroundManager.nextStage == Stage.Sunkentown)
        {
            StartCoroutine("ChangeTextColor");
        }
    }

    // 背景が黒いステージでテキストを白くするためのフェード
    IEnumerator ChangeTextColor()
    {
        for (float color = 0; color <= 1; color += 0.01f)
        {
            yield return new WaitForSeconds(0.01f);
            Color newColor = Color.white * color;
            newColor.a = 1;
            highscoreText.color = newColor;
        }
    }
}