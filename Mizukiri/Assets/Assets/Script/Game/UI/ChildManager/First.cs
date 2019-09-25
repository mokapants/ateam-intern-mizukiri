using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class First : MonoBehaviour
{
	UIManager uiManager;
	[SerializeField] Image stoneImage;

	void Start()
	{
		uiManager = transform.parent.gameObject.GetComponent<UIManager>();
		stoneImage.sprite = Stone.spriteRenderer.sprite;
	}

	void Update()
	{
		stoneImage.sprite = Stone.spriteRenderer.sprite;
	}

	public void OnSelectStone()
	{
		uiManager.OnSelectStone();
	}
}