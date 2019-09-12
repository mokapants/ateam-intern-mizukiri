using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public RigidCont stone_script;

    // Use this for initialization
    void Start () {
        
	}

    // Update is called once per frame
    void Update()
    {
        Vector3 stone_pos = stone_script.GetStonePos();
        int score = Mathf.FloorToInt(stone_pos.x);

        score = score / 70;
        this.GetComponent<Text>().text =
            "飛距離" + score.ToString() + "m";
    }
}
