using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField] GameObject onGame;
	[SerializeField] GameObject selectStone;
	[SerializeField] GameObject gameEnd;

	// Use this for initialization
	void Start()
	{
		onGame.SetActive(true);
		selectStone.SetActive(false);
		gameEnd.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void GameEnd()
	{
		onGame.SetActive(false);
		gameEnd.SetActive(true);
	}
}