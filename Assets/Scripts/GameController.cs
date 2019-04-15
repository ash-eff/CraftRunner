using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float spawnTimer;
    [SerializeField]
    private int changeTimer;
    [SerializeField]
    private int adjustAmount;

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public float SpawnTimer
    {
        get { return spawnTimer; }
        set { spawnTimer = value; }
    }

    private void Update()
    {
        if (Time.time > changeTimer)
        {
            changeTimer += adjustAmount;
            float adjust = spawnTimer * .2f;
            float speedAdj = speed + .5f;

            if (spawnTimer - adjust <= .5f)
            {
                spawnTimer = .5f;
            }
            else
            {
                spawnTimer -= adjust;
            }

            if(speed > 10f)
            {
                speed = 10f;
            }
            else
            {
                speed = speedAdj;
            }
        }
    }
}
