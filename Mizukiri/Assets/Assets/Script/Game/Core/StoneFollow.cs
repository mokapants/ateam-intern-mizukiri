using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneFollow : MonoBehaviour
{
    [SerializeField] RigidController rigidController;
    [SerializeField] float diffrence = 3;

    // Update is called once per frame
    void Update()
    {
        //石の座標と同期
        Vector3 position = transform.position;
        Vector3 stonePosition = rigidController.GetStonePos();
        position.x = stonePosition.x + diffrence;
        transform.localPosition = position;
    }
}