using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UnlockType
{
	Default,
	HighScore,
	ChallengeCount
}

public class UnlockData
{
	public int[] HighScore;
	public int[] ChallengeCount;
}

public class SelectStoneManager : MonoBehaviour
{
	UIManager uiManager;
	Stone stone;

	[SerializeField] Transform content;
	[SerializeField] Scrollbar scrollbar;
	[SerializeField] Text pleaseUnlockText;
	[SerializeField] Text unlockText;

	float oneEquallyDividedValue;

	public static UnlockData unlockData;

	void Start()
	{
		uiManager = transform.parent.gameObject.GetComponent<UIManager>();
		stone = GameObject.Find("stone(Clone)").GetComponent<Stone>();

		int counter = -1;
		bool isNextType = false;
		foreach (Transform stone in content)
		{
			string name = stone.name;
			if (counter == -1)
			{
				SetStatus(UnlockType.Default, 0, name);
			}
			else if (counter < unlockData.HighScore.Length && !isNextType)
			{
				SetStatus(UnlockType.HighScore, unlockData.HighScore[counter], name);
			}
			else
			{
				if (!isNextType)
				{
					isNextType = true;
					counter = 0;
				}
				SetStatus(UnlockType.ChallengeCount, unlockData.ChallengeCount[counter], name);
			}
			counter++;
		}

		stone.ChangeSkin(HttpCommunication.stones);

		oneEquallyDividedValue = 1f / (content.childCount - 1);
	}

	// Update is called once per frame
	void Update()
	{
		// 自動で近い石にカーソルを当てる
		if (0 == Input.touchCount && !Input.GetMouseButton(0))
		{
			float value = scrollbar.value;
			float remainder = value % oneEquallyDividedValue;
			if (remainder <= oneEquallyDividedValue / 2)
			{
				scrollbar.value -= remainder;
			}
			else
			{
				scrollbar.value += oneEquallyDividedValue - remainder;
			}

			float cursor = (Math.Abs(content.gameObject.GetComponent<RectTransform>().anchoredPosition.x)) + 128;

			int stoneNumber = 0;
			int mostNear = 512;
			foreach (Transform stone in content)
			{
				int pos = (int) Math.Abs(cursor - (Math.Abs(stone.gameObject.GetComponent<RectTransform>().anchoredPosition.x)));
				if (pos < mostNear)
				{
					stoneNumber = int.Parse(stone.name.Replace("(Clone)", ""));
					mostNear = pos;
				}
			}

			int counter = -1;
			bool isNextType = false;
			for (int i = 0; i <= stoneNumber; i++)
			{
				if (counter == -1 && stoneNumber == 0)
				{
					unlockText.text = "普通の石";
					return;
				}

				if (i == stoneNumber)
				{
					if (!isNextType)
					{
						// ハイスコア
						Debug.Log(i + " : " + stoneNumber + " : " + counter);
						unlockText.text = "1回で" + unlockData.HighScore[counter].ToString() + "m飛ぶ";
						return;
					}
					else
					{
						// チャレンジ回数
						Debug.Log(i + " : " + stoneNumber + " : " + counter);
						unlockText.text = unlockData.ChallengeCount[counter].ToString() + "回プレイする";
						return;
					}
				}

				if (unlockData.HighScore.Length - 1 <= counter)
				{
					if (!isNextType)
					{
						isNextType = true;
						counter = 0;
						continue;
					}
				}
				counter++;
			}
		}
	}

	void SetStatus(UnlockType type, int value, string stoneName)
	{
		RawImage stoneImage = content.Find(stoneName).GetComponent<RawImage>();

		stoneImage.color = Color.white;
		if (PlayerPrefs.GetInt(stoneName, 0) == 1)
		{
			return;
		}
		if (type == UnlockType.ChallengeCount)
		{
			// 回数超えてなかったらダメ判定
			if (PlayerPrefs.GetInt("ChallengeCount", 0) < value)
			{
				float hard = 0.2f;
				stoneImage.color *= hard;
				return;
			}
		}
		else if (type == UnlockType.HighScore)
		{
			// 回数超えてなかったらダメ判定
			if (PlayerPrefs.GetInt("HighScore", 0) < value)
			{
				float hard = 0.2f;
				stoneImage.color *= hard;
				return;
			}
		}

		PlayerPrefs.SetInt(stoneName, 1);
	}

	public void OnBackButton()
	{
		uiManager.OffSelectStone();
	}

	public void OnSelectButton()
	{
		float cursor = (Math.Abs(content.gameObject.GetComponent<RectTransform>().anchoredPosition.x)) + 128;
		Debug.Log(cursor);

		int stoneNumber = 0;
		int mostNear = 512;
		foreach (Transform stone in content)
		{
			int pos = (int) Math.Abs(cursor - (Math.Abs(stone.gameObject.GetComponent<RectTransform>().anchoredPosition.x)));
			if (pos < mostNear)
			{
				stoneNumber = int.Parse(stone.name.Replace("(Clone)", ""));
				mostNear = pos;
			}
		}

		if (PlayerPrefs.GetInt(stoneNumber + "(Clone)", 0) == 0)
		{
			StartCoroutine("PleaseUnlock");
			Debug.Log("使えないよ");
			return;
		}

		Stone.textureNumber = stoneNumber;
		stone.ChangeSkin(HttpCommunication.stones);
		PlayerPrefs.SetInt("StoneSkin", stoneNumber);
	}

	IEnumerator PleaseUnlock()
	{
		pleaseUnlockText.color = Color.white;
		yield return new WaitForSeconds(0.5f);
		for (float color = 1; 0 <= color; color -= 0.01f)
		{
			yield return new WaitForSeconds(0.01f);
			Color newColor = Color.white * color;
			pleaseUnlockText.color = newColor;
		}
	}
}