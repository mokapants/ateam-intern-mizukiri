using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour {


    public RigidCont stone_sctipt;

    ////[Range(0.1f, 1.0f)]
    //public float scroll_speed;


    [Range(0.0f, 1000f)]
    public float scroll_area;

    [Range(-10f, 10f)]
    public float scroll_pos_y;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        //石の座標と同期
        Vector3 stone_pos = stone_sctipt.GetStonePos();
        float stone_speed = stone_sctipt.GetStoneSpeed();


        Debug.Log(stone_speed);
        if (stone_sctipt.GetStatFlag())
        {
            //スクロール
            transform.Translate(-stone_speed/1000, 0, 0);
            if (transform.position.x < stone_pos.x - scroll_area / 2)
            {
                transform.position = new Vector3(stone_pos.x + scroll_area, scroll_pos_y);
            }
        }
    }
}
