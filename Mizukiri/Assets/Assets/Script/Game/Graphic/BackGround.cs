using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{

    public RigidController rigidController;

    ////[Range(0.1f, 1.0f)]
    //public float scroll_speed;

    [Range(0.0f, 1000f), SerializeField] float scrollArea;
    [Range(-10f, 10f), SerializeField] float scrollPositionY;

    void Update()
    {
        //石の座標と同期
        Vector3 stonePosition = rigidController.GetStonePos();
        float stoneSpeed = rigidController.GetStoneSpeed();

        Debug.Log(stoneSpeed);
        if (rigidController.GetStatFlag())
        {
            //スクロール
            transform.Translate(-stoneSpeed / 1000, 0, 0);
            if (transform.position.x < stonePosition.x - scrollArea / 2)
            {
                transform.position = new Vector3(stonePosition.x + scrollArea, scrollPositionY);
            }
        }
    }
}