using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
	GameManager gameManager;
	EffectsManager effectsManager;
	Rigidbody2D rigidbody2d;
	//AudioClip soundEffect_bound;
	AudioSource audioSource;

	[SerializeField] ParticleSystem accelEffect;

	float StartPower; // 初速
	float MaxPower; // 最大スピード
	float power; // 現在のスピード
	float LowestPower; // スピードの下限
	float UpwardQuantity; // アクセル時の加速量
	float FallQuantity; // グッドの時の減速量

	float AngleRange; // 飛ぶ角度
	float angle;
	public float Angle
	{
		get { return angle + 2f; }
	}

	bool[] hitFlag; // グッドとパーフェクトの当たり判定
	public static int consecutive; // 連続パーフェクトのカウンター
	float AngleForPower; // 1の速度に対する角度の変化量

	void Awake()
	{
		gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
		effectsManager = GameObject.Find("Manager").GetComponent<EffectsManager>();

		rigidbody2d = GetComponent<Rigidbody2D>();
		rigidbody2d.simulated = false;
		audioSource = GetComponent<AudioSource>();

		GameStatusData gameStatus = Resources.Load<GameStatusData>("data/core/GameStatus");

		StartPower = gameStatus.startPower;
		power = StartPower;
		LowestPower = gameStatus.lowestPower;
		UpwardQuantity = gameStatus.upwardQuantity;
		FallQuantity = gameStatus.fallQuantity;
		AngleRange = gameStatus.angleRange;
		angle = AngleRange;

		MaxPower = (UpwardQuantity * 10) + StartPower;
		AngleForPower = Angle / (MaxPower - LowestPower);

		hitFlag = new bool[2] { false, false };
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
				accelEffect.Play();
				rigidbody2d.simulated = true;
				rigidbody2d.AddForce(new Vector2(StartPower, Angle), ForceMode2D.Impulse);
				Debug.Log(rigidbody2d.velocity);

				GameManager.isStart = true;
			}
			else
			{
				Vector2 stonePosition = transform.position;
				Effect type = Effect.Normal;
				if (hitFlag[0]) // パーフェクト
				{
					audioSource.Play();
					if (4 <= consecutive)
					{
						// アクセルの処理
						if (power <= MaxPower - UpwardQuantity)
						{
							power += UpwardQuantity;
						}
						else
						{
							power = MaxPower;
						}

						// 跳ねる高さの調整の処理
						angle = (MaxPower - power) * AngleForPower;

						accelEffect.Play();

						consecutive = 0;
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
					audioSource.Play();
					// 減速の処理
					power -= FallQuantity;

					// 跳ねる高さの調整の処理
					angle = (MaxPower - power) * AngleForPower;

					if (power < LowestPower)
					{
						gameManager.GameEnd();
						return;
					}

					consecutive = 0;
				}
				else // ミス
				{
					// 終了の処理
					gameManager.GameEnd();
					return;
				}

				effectsManager.PlayEffect(type, stonePosition);

				rigidbody2d.velocity = Vector2.zero;
				rigidbody2d.AddForce(new Vector2(power, Angle), ForceMode2D.Impulse);
			}
			//Debug.Log(power);
			Debug.Log(Angle);
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