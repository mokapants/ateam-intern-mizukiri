using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BackLayer
{
    near = 1,
    middle,
    far
}

public class BackGround : MonoBehaviour
{
    public const float width = 57.60f;
    public const float height = 10.80f;

    private BackLayer layer;
    public BackLayer Layer
    {
        get { return layer; }
    }

    void Awake()
    {
        string tag = gameObject.tag;
        switch (tag)
        {
            case "near":
                layer = (BackLayer) 1;
                break;

            case "middle":
                layer = (BackLayer) 2;
                break;

            case "far":
                layer = (BackLayer) 3;
                break;
        }
        gameObject.transform.position += Vector3.forward * (int) layer;
    }
}