using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField] GameObject gameEnd;

	// Use this for initialization
	void Start()
	{
		gameEnd.GetComponent<Canvas>().enabled = false;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void GameEnd()
	{
		gameEnd.GetComponent<Canvas>().enabled = true;
		gameEnd.GetComponent<Animator>().enabled = true;
	}
}