using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndManager : MonoBehaviour
{
	[SerializeField] Text scoreText;
	[SerializeField] Text highscoreText;

	void Start()
	{
		scoreText.text = GameManager.score.ToString() + " m";
		highscoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString() + " m";
	}
}