using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		float x = GameObject.Find("stone(Clone)").transform.position.x + 100;
		Vector3 position = new Vector3(x, -3.3f, 1);
		transform.position = position;
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 newPosition = transform.position;
		newPosition.x -= 0.01f;
		transform.position = newPosition;

		float x = GameObject.Find("stone(Clone)").transform.position.x;
		if (newPosition.x < x - 50)
		{
			Destroy(gameObject);
		}
	}
}