using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Effect
{
	Normal,
	Perfect
}

public class EffectsManager : MonoBehaviour
{
	GameObject effectObject;
	GameObject[] effects = new GameObject[2];

	void Awake()
	{
		effectObject = Resources.Load<GameObject>("prefabs/effects/Effects");
		effectObject = Instantiate(effectObject);
		effects[0] = effectObject.transform.Find("NormalEffect").gameObject; // 普通の水パシャ
		effects[1] = effectObject.transform.Find("PerfectEffect").gameObject;
	}

	public void PlayEffect(Effect type, Vector2 position)
	{
		effects[(int) type].transform.position = position;
		effects[(int) type].GetComponent<ParticleSystem>().Play();
	}

}