using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	GameObject stone;

	float differenceFromCenter;

	// Use this for initialization
	void Start()
	{
		stone = GameObject.FindWithTag("Stone");

		differenceFromCenter = 2;
	}

	// Update is called once per frame
	void Update()
	{
		if (stone.transform.position.x < differenceFromCenter)
		{
			return;
		}

		Vector3 newPosition = Vector3.zero;
		newPosition.x += stone.transform.position.x + differenceFromCenter;
		newPosition.z = gameObject.transform.position.z;
		if (gameObject.transform.position.z < -7)
		{
			iTween.Stop(gameObject);
			newPosition.z = -7;
		}
		gameObject.transform.position = newPosition;
	}

	public IEnumerator AccelMotion()
	{
		Debug.Log("yoba");
		Time.timeScale = 0.7f;
		iTween.PunchPosition(gameObject, iTween.Hash("x", -1, "z", 2, "time", 3f, "delay", 0.01f));

		yield return new WaitForSecondsRealtime(0.6f);

		Time.timeScale = 1;
	}
}