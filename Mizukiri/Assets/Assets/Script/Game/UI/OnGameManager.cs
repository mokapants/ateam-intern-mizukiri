using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnGameManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text highscoreText;
    [SerializeField] Text comboText;
    [SerializeField] Animator comboAnime;

    void Awake()
    {
        comboAnime.transform.gameObject.SetActive(false);
    }

    void Start()
    {
        highscoreText.text = PlayerPrefs.GetInt("highscore", 0).ToString() + " m";
    }

    void Update()
    {
        if (GameManager.isEnd)
        {
            return;
        }

        scoreText.text = GameManager.score.ToString() + " m";

        if (BackGroundManager.nextStage == Stage.Sunkentown)
        {
            StartCoroutine("ChangeTextColor");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (2 <= Stone.consecutive)
            {
                comboAnime.transform.gameObject.SetActive(false);
                comboAnime.transform.gameObject.SetActive(true);
                comboText.enabled = true;
                comboText.text = "Perfect × " + Stone.consecutive;
            }
            else
            {
                comboAnime.transform.gameObject.SetActive(false);
                comboText.enabled = false;
            }
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
            scoreText.color = newColor;
        }
    }
}