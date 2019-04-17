using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    public Transform[] foreGround;
    public Transform[] backGround;

    private float foreGroundReset;
    private Vector2 foreGroundStart;


    private void Awake()
    {
        foreGroundStart = new Vector2(45.64f, 1.5f);
        foreGroundReset = -(Camera.main.aspect * Camera.main.orthographicSize) - (foreGround[0].localScale.x / 2);
    }

    private void Update()
    {
        if(foreGround[0].position.x < foreGroundReset)
        {
            foreGround[0].position = foreGroundStart;
        }
    }
}
