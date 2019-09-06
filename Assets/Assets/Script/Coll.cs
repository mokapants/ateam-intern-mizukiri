using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coll : MonoBehaviour {


    public GameObject stone;


    // Use this for initialization
    void Start () {
       
        stone = GameObject.Find("Stone");
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.localPosition = new Vector3(stone.transform.position.x,0,0);

    }
}
