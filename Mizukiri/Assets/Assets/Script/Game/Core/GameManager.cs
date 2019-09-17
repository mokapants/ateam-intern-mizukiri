using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] RigidController rigidController;

	Vector3 stonePosition;
	public static int score;

	void Awake()
	{
		GameObject backGround = Resources.Load<GameObject>("prefabs/background/BackGround");
		Instantiate(backGround);
	}

	// Use this for initialization
	void Start()
	{
		score = Mathf.FloorToInt(stonePosition.x);
	}

	// Update is called once per frame
	void Update()
	{
		stonePosition = rigidController.GetStonePos();
		score = Mathf.FloorToInt(stonePosition.x);

		if (score > 50)
		{
			BackGroundManager.nextStage = Stage.Sea;
		}
	}
}