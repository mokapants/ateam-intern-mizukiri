using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField] GameObject firstAndOnGame;
	[SerializeField] GameObject onGame;
	[SerializeField] GameObject selectStone;
	[SerializeField] GameObject gameEnd;

	// Use this for initialization
	void Start()
	{
		firstAndOnGame.SetActive(true);
		onGame.SetActive(false);
		selectStone.SetActive(false);
		gameEnd.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		if (GameManager.isStart)
		{
			onGame.SetActive(true);
		}

		if (GameManager.canRestart)
		{
			onGame.SetActive(false);
			gameEnd.SetActive(true);
		}
	}
}