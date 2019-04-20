using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienFloat : MonoBehaviour
{
    [SerializeField]
    private float amplitude = 2f;
    [SerializeField]
    private float period = 1f;

    private GameController gc;
    private Camera cam;
    private AudioSource audioSource;

    private float widthAndOffset;

    protected void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gc = FindObjectOfType<GameController>();
        cam = Camera.main;
        widthAndOffset = cam.aspect * cam.orthographicSize * 2f;
        transform.position = new Vector3(widthAndOffset, 0f);
        StartCoroutine(MakeNoise());
    }

    protected void Update()
    {
        widthAndOffset = cam.aspect * cam.orthographicSize * 2f;
        float theta = Time.timeSinceLevelLoad / period;
        float distance = amplitude * Mathf.Sin(theta);
        float xAxis = transform.position.x + -1 * gc.Speed * Time.deltaTime;
        float yAxis = 1 * distance;
        transform.position = new Vector2(xAxis, yAxis);

        if (transform.position.x < -widthAndOffset)
        {
            Destroy(gameObject);
        }
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
