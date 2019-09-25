using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndManager : MonoBehaviour
{
	[SerializeField] Text scoreText;
	[SerializeField] Text highscoreText;
	[SerializeField] Image stoneImage;

	void Start()
	{
		scoreText.text = GameManager.score.ToString() + " m";
		highscoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString() + " m";
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		stoneImage.sprite = Stone.spriteRenderer.sprite;
	}
}