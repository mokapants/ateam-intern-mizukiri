using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class First : MonoBehaviour
{
	Canvas firstCanvas;

	void Start()
	{
		firstCanvas = GetComponent<Canvas>();
		firstCanvas.enabled = true;
	}

	void Update()
	{
		if (GameManager.isStart)
		{
			firstCanvas.enabled = false;
		}
	}
}