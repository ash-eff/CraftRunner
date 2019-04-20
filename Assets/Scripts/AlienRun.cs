using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienRun : MonoBehaviour
{
    private float screenWidthAndOffset;

    private Camera cam;
    private GameController gc;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gc = FindObjectOfType<GameController>();
        cam = Camera.main;
        screenWidthAndOffset = cam.aspect * cam.orthographicSize + 2f;
        transform.position = new Vector3(screenWidthAndOffset, -3f);
        StartCoroutine(MakeNoise());
    }

    private void Update()
    {
        transform.Translate(Vector3.left * (gc.Speed - 2f) * Time.deltaTime);
    }
    
    IEnumerator MakeNoise()
    {
        yield return new WaitForSeconds(.5f);
        while (true)
        {
            audioSource.Play();

            yield return new WaitForSeconds(2f);
        }
    }
}
