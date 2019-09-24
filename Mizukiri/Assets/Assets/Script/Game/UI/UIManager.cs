using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField] GameObject first;
	[SerializeField] GameObject firstAndOnGame;
	[SerializeField] GameObject onGame;
	[SerializeField] public GameObject selectStone;
	[SerializeField] GameObject gameEnd;

	// スクリプトから最初に取得するときのため
	void Awake()
	{
		onGame.SetActive(true);
	}

	// Use this for initialization
	void Start()
	{
		first.SetActive(true);
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
			first.SetActive(false);
			onGame.SetActive(true);
		}

		if (GameManager.canRestart)
		{
			onGame.SetActive(false);
			gameEnd.SetActive(true);
		}
	}

	public void OnSelectStone()
	{
		first.SetActive(false);
		firstAndOnGame.SetActive(false);
		selectStone.SetActive(true);
	}

	public void OffSelectStone()
	{
		first.SetActive(true);
		firstAndOnGame.SetActive(true);
		selectStone.SetActive(false);
	}
}