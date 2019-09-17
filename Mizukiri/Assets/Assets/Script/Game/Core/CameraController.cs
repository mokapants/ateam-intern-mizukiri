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

		differenceFromCenter = 3;
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 newPosition = Vector3.zero;
		newPosition.x += stone.transform.position.x + differenceFromCenter;
		newPosition.z = gameObject.transform.position.z;
		gameObject.transform.position = newPosition;
	}
}