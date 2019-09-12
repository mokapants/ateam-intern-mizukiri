using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneFollow : MonoBehaviour
{
    [SerializeField] RigidController rigidController;
    private Vector2 position;

    // Use this for initialization
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //石の座標と同期
        Vector3 stonePosition = rigidController.GetStonePos();
        position.x += stonePosition.x;
        transform.localPosition = position;
    }
}