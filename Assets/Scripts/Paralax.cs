using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    public float speed;
    private float foreGroundReset = -27.5f;
    private float foreGroundStart = 44f;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.position.x < foreGroundReset)
        {
            //speed = 0;
            transform.position = new Vector2(foreGroundStart, transform.position.y);
        }
    }
}
