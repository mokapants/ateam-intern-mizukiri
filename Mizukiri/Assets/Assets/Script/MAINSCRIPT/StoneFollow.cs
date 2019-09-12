using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneFollow : MonoBehaviour {

    public RigidCont stone_script;
    
    private Vector3 pos;
    // Use this for initialization
    void Start () {
        pos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //石の座標と同期
        Vector3 stone_pos = stone_script.GetStonePos();

        this.transform.localPosition= new Vector3(pos.x + stone_pos.x, pos.y, pos.z);
    }
}
