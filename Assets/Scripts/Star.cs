using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private GameController gc;
    private Camera cam;

    private void Awake()
    {
        gc = FindObjectOfType<GameController>();
        cam = Camera.main;
    }

    void Update()
    {
        transform.Translate(Vector2.left * gc.Speed * Time.deltaTime);

        if(transform.position.x < -(cam.aspect * cam.orthographicSize + 1))
        {
            Destroy(gameObject);
        }
    }
}
