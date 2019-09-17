using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	GameObject ui;

	public static bool isStart;
	public static bool isEnd;
	bool canRestart;

	Vector3 stonePosition;
	public static int score;

	void Awake()
	{
		ui = Resources.Load<GameObject>("prefabs/ui");
		ui = Instantiate(ui);
		GameObject backGround = Resources.Load<GameObject>("prefabs/background/BackGround");
		Instantiate(backGround);

		isStart = false;
		isEnd = false;
		canRestart = false;
	}

	// Use this for initialization
	void Start()
	{
		score = Mathf.FloorToInt(stonePosition.x);
	}

	// Update is called once per frame
	void Update()
	{
		// stonePosition = rigidController.GetStonePos();
		// score = Mathf.FloorToInt(stonePosition.x);

		// if (score > 50)
		// {
		// 	BackGroundManager.nextStage = Stage.Sea;
		// }

		if (canRestart)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				SceneManager.LoadScene("ForOkae");
			}
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
		ui.GetComponent<UIManager>().GameEnd();
		canRestart = true;
	}
}