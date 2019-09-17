﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameStatusData : ScriptableObject
{
	public float startPower = 20f; // 最初の投石の強さ
	public float upwardQuantity = 2f; // アクセル時の上昇する速度
	public float fallQuantity = 5f; // 減速時の減速する速度
	public float heightRange = 3f; // 跳ねる時のy軸の角度
}