using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public RigidController rigidController;

    void Update()
    {
        Vector3 stonePosition = rigidController.GetStonePos();
        int score = Mathf.FloorToInt(stonePosition.x);

        score = score / 70;
        this.GetComponent<Text>().text = "飛距離" + score.ToString() + "m";
    }
}