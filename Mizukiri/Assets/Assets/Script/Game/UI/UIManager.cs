using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField] GameObject nowLoading;
	[SerializeField] GameObject first;
	[SerializeField] GameObject firstAndOnGame;
	[SerializeField] GameObject onGame;
	[SerializeField] public GameObject selectStone;
	[SerializeField] GameObject gameEnd;

	static bool firstLoadOnly = true;

	// スクリプトから最初に取得するときのため
	void Awake()
	{
		nowLoading.SetActive(true);
		first.SetActive(true);
		firstAndOnGame.SetActive(true);
		onGame.SetActive(true);
		selectStone.SetActive(true);
		gameEnd.SetActive(true);
	}

	// Use this for initialization
	void Start()
	{
		if (firstLoadOnly)
		{
			nowLoading.SetActive(true);
			first.SetActive(false);
			firstAndOnGame.SetActive(false);
			onGame.SetActive(false);
			selectStone.SetActive(false);
			gameEnd.SetActive(false);
		}
		else
		{
			nowLoading.SetActive(false);
			first.SetActive(true);
			firstAndOnGame.SetActive(true);
			onGame.SetActive(false);
			selectStone.SetActive(false);
			gameEnd.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (!GameManager.isLoad && firstLoadOnly)
		{
			firstLoadOnly = false;
			nowLoading.SetActive(false);
			first.SetActive(true);
			firstAndOnGame.SetActive(true);
		}
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