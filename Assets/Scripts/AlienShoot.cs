using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShoot : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private ParticleSystem warningPart;
    [SerializeField]
    private GameObject laser;

    private float stopPosX;
    private float screenWidthAndOffset;

    private bool moving = true;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        screenWidthAndOffset = cam.aspect * cam.orthographicSize + 2f;
        stopPosX = cam.aspect * cam.orthographicSize - 1f;
        transform.position = new Vector3(screenWidthAndOffset, -3.5f);
    }

    void Update()
    {
        if(transform.position.x > stopPosX && moving)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else if (moving)
        {
            moving = false;
            StartCoroutine(Shoot());
        }

        if(transform.position.y > cam.orthographicSize + 1)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Shoot()
    {
        bool shooting = true;
        while(shooting)
        {
            warningPart.Play();
            yield return new WaitForSeconds(1f);
            laser.SetActive(true);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.left, 25f);
            Debug.DrawRay(transform.position, Vector3.left * 25f, Color.red, 1f);
            if (hit)
            {
                if(hit.transform.tag == "Player")
                {
                    Destroy(hit.transform.gameObject);
                }
            }
            yield return new WaitForSeconds(1f);
            laser.SetActive(false);
            shooting = false;
        }
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        float newPosY = transform.position.y + 3.5f;
        while (transform.position.y < newPosY)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            yield return null;
        }
        StartCoroutine(Shoot());
    }
}
