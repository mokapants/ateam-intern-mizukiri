using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnGameManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text comboText;
    [SerializeField] Animator comboAnime;

    bool isAlreadyChangeColor;

    float beforePower;

    void Awake()
    {
        comboAnime.transform.gameObject.SetActive(false);

        isAlreadyChangeColor = false;
    }

    void Start()
    {
        scoreText.text = "0 m";
        StartCoroutine("FadeinText");
    }

    void Update()
    {
        if (0 < GameManager.score)
        {
            scoreText.text = GameManager.score.ToString() + " m";
        }

        if (BackGroundManager.nextStage == Stage.Sunkentown && !isAlreadyChangeColor)
        {
            StartCoroutine("ChangeTextColor");
        }

        if (Stone.consecutive == 0)
        {
            comboAnime.transform.gameObject.SetActive(false);
            comboText.enabled = false;
        }
    }

    public void SetComboText()
    {
        comboAnime.transform.gameObject.SetActive(false);
        comboAnime.transform.gameObject.SetActive(true);
        comboText.enabled = true;
        comboText.text = "PERFECT × " + Stone.consecutive;
    }

    public void SetAccelText()
    {
        comboAnime.transform.gameObject.SetActive(false);
        comboAnime.transform.gameObject.SetActive(true);
        comboText.enabled = true;
        comboText.text = "BOOST!!";
    }

    // スタート時のフェードイン
    IEnumerator FadeinText()
    {
        for (float color = 0; color <= 1; color += 0.01f)
        {
            yield return new WaitForSeconds(0.01f);
            Color newColor = scoreText.color;
            newColor.a = color;
            scoreText.color = newColor;
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
            scoreText.color = newColor;
        }
    }
}