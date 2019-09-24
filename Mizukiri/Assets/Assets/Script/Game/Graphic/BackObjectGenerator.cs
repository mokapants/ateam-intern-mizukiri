using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackObjectGenerator : MonoBehaviour
{
	[SerializeField] GameObject[] fishes;
	Transform nextWave;

	void Start()
	{
		StartCoroutine("Generator");
	}

	IEnumerator Generator()
	{
		while (true)
		{
			yield return new WaitForSeconds(1f);
			if (!GameManager.isStart || GameManager.isEnd)
			{
				continue;
			}

			if (Random.Range(0, 5) < 2)
			{
				int fishType = Random.Range(0, 2);
				Instantiate(fishes[fishType]);
			}
		}
	}
}