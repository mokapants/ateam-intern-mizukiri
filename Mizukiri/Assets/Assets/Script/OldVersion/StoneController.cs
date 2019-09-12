using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneController : MonoBehaviour
{

    float speed;

    float gravity;

    Vector3 vel;
    Vector3 Pos;
    Vector3 beforePos;

    int score;

    // Use this for initialization
    void Start()
    {
        speed = 10;

        gravity = 0;
        vel.x = 0;
        vel.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        vel.x = 0;
        vel.y = 0;

        score = Mathf.FloorToInt(this.transform.position.x);

        Debug.Log(score);

        if ((this.transform.position.y < 0.3) && (this.transform.position.y > -0.1f))
        {

            Vector2 direction = this.transform.position - beforePos;
            Vector2 foward;
            foward.x = direction.x;
            foward.y = 0;

            float angle = Mathf.Acos(Vector2.Dot(foward, direction) / (direction.magnitude * foward.magnitude));
            angle *= Mathf.Rad2Deg;

            Vector2 Unit_a;
            Unit_a.x = Mathf.Cos(angle) * vel.x;
            Unit_a.y = Mathf.Sin(angle);

            gravity = 0;

        }

        beforePos = this.transform.position;

        vel.x += speed / 60;
        vel.x *= 0.98f;

        gravity = -0.04f;
        vel.y += gravity;

        Debug.Log(gravity);

        this.transform.Translate(vel);
    }

}