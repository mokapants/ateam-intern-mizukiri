using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UnlockType
{
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

	[SerializeField] Transform content;
	[SerializeField] Scrollbar scrollbar;

	float oneEquallyDividedValue;

	public static UnlockData unlockData;

	void Start()
	{
		uiManager = transform.parent.gameObject.GetComponent<UIManager>();

		int counter = 0;
		bool isNextType = false;
		foreach (Transform stone in content)
		{
			string name = stone.name;
			if (counter < unlockData.HighScore.Length && !isNextType)
			{
				SetStatus(UnlockType.HighScore, unlockData.HighScore[counter], name);
			}
			else
			{
				isNextType = true;
				counter = 0;
				SetStatus(UnlockType.ChallengeCount, unlockData.ChallengeCount[counter], name);
			}
		}

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
		}
	}

	void SetStatus(UnlockType type, int value, string stoneName)
	{
		RawImage stoneImage = content.Find(stoneName).GetComponent<RawImage>();
		bool isRelease = PlayerPrefs.GetInt(stoneName, 0) == 0 ? false : true;

		stoneImage.color = Color.white;
		if (type == UnlockType.ChallengeCount)
		{
			// 回数超えてなかったらダメ判定
			if (PlayerPrefs.GetInt("ChallengeCount", 0) < value)
			{
				float hard = 0.2f;
				stoneImage.color *= hard;
			}
		}
		else
		{
			// 回数超えてなかったらダメ判定
			if (PlayerPrefs.GetInt("HighScore", 0) < value)
			{
				float hard = 0.2f;
				stoneImage.color *= hard;
			}
		}
	}

	public void OnBackButton()
	{
		uiManager.OffSelectStone();
	}
}