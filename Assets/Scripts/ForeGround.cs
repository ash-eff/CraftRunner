using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForeGround : MonoBehaviour
{
    private float rightOffScreen;
    private float leftOffScreen;

    private GameController gc;
    private Collider2D col;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        col = GetComponent<Collider2D>();
        gc = FindObjectOfType<GameController>();
        leftOffScreen = -(cam.aspect * cam.orthographicSize) - col.bounds.size.x / 2;
        rightOffScreen = (cam.aspect * cam.orthographicSize) + (col.bounds.size.x - 2f);
    }

    void Update()
    {
        transform.Translate(Vector2.left * gc.Speed * Time.deltaTime);
        if (transform.position.x < leftOffScreen)
        {
            transform.position = new Vector2(rightOffScreen, transform.position.y);
        }
    }
}
