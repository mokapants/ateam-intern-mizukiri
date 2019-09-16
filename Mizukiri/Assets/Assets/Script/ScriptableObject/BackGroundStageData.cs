using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stage
{
	River,
	RiverToSea,
	Sea
}

[CreateAssetMenu]
public class BackGroundStageData : ScriptableObject
{
	public List<BackGroundStage> backGroundStages = new List<BackGroundStage>();
}

[System.Serializable]
public class BackGroundStage
{
	public Stage name;
	public GameObject near;
	public GameObject middle;
	public GameObject far;
}