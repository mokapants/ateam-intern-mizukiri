using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectStoneManager : MonoBehaviour
{
	[SerializeField] Transform content;
	[SerializeField] Scrollbar scrollbar;

	float oneEquallyDividedValue;

	void Start()
	{
		PlayerPrefs.SetInt("default stone", 1);
		foreach (Transform stone in content)
		{
			string name = stone.name;
			SetStatus(name);
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

	void SetStatus(string stoneName)
	{
		Image stoneImage = content.Find(stoneName).GetComponent<Image>();
		bool isRelease = PlayerPrefs.GetInt(stoneName, 0) == 0 ? false : true;

		stoneImage.color = Color.white;
		if (!isRelease)
		{
			float hard = 0.2f;
			stoneImage.color *= hard;
		}
	}
}