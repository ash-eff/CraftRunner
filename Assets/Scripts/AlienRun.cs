using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienRun : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    private float screenWidthAndOffset;

    private Camera cam;
    private GameController gc;

    private void Start()
    {
        gc = FindObjectOfType<GameController>();
        cam = Camera.main;
        screenWidthAndOffset = cam.aspect * cam.orthographicSize + 2f;
        transform.position = new Vector3(screenWidthAndOffset, -3f);
    }

    private void Update()
    {
        transform.Translate(Vector3.left * gc.Speed * Time.deltaTime);
    }
}
