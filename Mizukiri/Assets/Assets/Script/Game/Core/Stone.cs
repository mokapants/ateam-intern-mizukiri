using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
	GameManager gameManager;
	EffectsManager effectsManager;
	Rigidbody2D rigidbody2d;

	float startPower;
	public float StartPower
	{
		get { return startPower + 10f; }
	}
	float power;
	public float Power
	{
		get { return power + 10f; }
	}
	float upwardQuantity;
	float fallQuantity;

	float heightRange;
	public float HeightRange
	{
		get { return heightRange + 2f; }
	}
	bool[] hitFlag;
	int perfectCounter;
	int consecutive;

	void Awake()
	{
		gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
		effectsManager = GameObject.Find("Manager").GetComponent<EffectsManager>();

		rigidbody2d = GetComponent<Rigidbody2D>();
		rigidbody2d.simulated = false;

		GameStatusData gameStatus = Resources.Load<GameStatusData>("data/core/GameStatus");

		startPower = gameStatus.startPower;
		power = startPower;
		upwardQuantity = gameStatus.upwardQuantity;
		fallQuantity = gameStatus.fallQuantity;
		heightRange = gameStatus.heightRange;

		hitFlag = new bool[2] { false, false };
		perfectCounter = 0;
		consecutive = 0;
	}

	void Update()
	{
		if (GameManager.isEnd)
		{
			return;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			// 最初の投石
			if (!GameManager.isStart)
			{
				rigidbody2d.simulated = true;
				rigidbody2d.AddForce(new Vector2(StartPower, HeightRange), ForceMode2D.Impulse);
				Debug.Log(rigidbody2d.velocity);

				GameManager.isStart = true;
			}
			else
			{
				Vector2 stonePosition = transform.position;
				Effect type = Effect.Normal;
				if (hitFlag[0]) // パーフェクト
				{
					if (perfectCounter < 10 && 4 < consecutive)
					{
						// アクセルの処理
						power += upwardQuantity;

						// 跳ねる高さを下げる処理
						heightRange -= heightRange / 10;

						consecutive = 0;
						perfectCounter++;
					}
					else
					{
						// スピード維持の処理
						consecutive++;
					}

					stonePosition = transform.position;
					type = Effect.Perfect;
				}
				else if (hitFlag[1]) // グッド
				{
					// 減速の処理
					power -= fallQuantity;

					consecutive = 0;
				}
				else // ミス
				{
					// 終了の処理
					power = 0;
					gameManager.GameEnd();
				}

				effectsManager.PlayEffect(type, stonePosition);

				rigidbody2d.velocity = Vector2.zero;
				rigidbody2d.AddForce(new Vector2(Power, HeightRange), ForceMode2D.Impulse);
			}
			Debug.Log(Power);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		string tag = other.tag;
		if (tag == "Perfect")
		{
			hitFlag[0] = true;
		}
		else if (tag == "Good")
		{
			hitFlag[1] = true;
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		string tag = other.tag;
		if (tag == "Perfect")
		{
			hitFlag[0] = true;
		}
		else if (tag == "Good")
		{
			hitFlag[1] = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		string tag = other.tag;
		if (tag == "Perfect")
		{
			hitFlag[0] = false;
		}
		else if (tag == "Good")
		{
			hitFlag[1] = false;
		}
	}
}