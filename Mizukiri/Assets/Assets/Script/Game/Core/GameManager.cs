using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	GameObject stone;
	GameObject ui;

	public static bool isStart;
	public static bool isEnd;
	public static bool canRestart;

	public static int score;

	void Awake()
	{
		stone = Resources.Load<GameObject>("prefabs/Stone");
		stone = Instantiate(stone);
		ui = Resources.Load<GameObject>("prefabs/ui");
		ui = Instantiate(ui);
		GameObject backGround = Resources.Load<GameObject>("prefabs/background/BackGround");
		Instantiate(backGround);

		isStart = false;
		isEnd = false;
		canRestart = false;

		score = 0;
	}

	void Update()
	{
		if (canRestart)
		{
			if ((Input.GetKeyDown(KeyCode.Space)) || (Input.GetMouseButtonDown(0)))
			{
				SceneManager.LoadScene("Game");
			}
		}

		if (isEnd)
		{
			return;
		}

		int stonePositionX = (int) stone.transform.position.x;
		score = stonePositionX;

		if (1500 < score)
		{
			BackGroundManager.nextStage = Stage.Milkyway;
		}
		else if (800 < score)
		{
			BackGroundManager.nextStage = Stage.Sunkentown;
		}
		else if (150 < score)
		{
			BackGroundManager.nextStage = Stage.Sea;
		}
	}

	public void GameEnd()
	{
		isEnd = true;
		StartCoroutine("GameEndCoroutine");
	}

	IEnumerator GameEndCoroutine()
	{
		yield return new WaitForSeconds(1f);

		// ハイスコア更新用
		int highscore = PlayerPrefs.GetInt("highscore", 0);
		if (highscore < score)
		{
			PlayerPrefs.SetInt("highscore", score);
		}

		canRestart = true;
	}
}