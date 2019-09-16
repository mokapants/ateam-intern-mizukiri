using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public RigidController rigidController;

    void Update()
    {
        int score = GameManager.score;
        GetComponent<Text>().text = "飛距離" + score.ToString() + "m";
    }
}