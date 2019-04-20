using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShoot : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject sparks;
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private LayerMask damageLayer;

    private float stopPosX;
    private float screenWidthAndOffset;

    private bool moving = true;

    private AudioSource audioSource;
    private GameController gc;
    private Camera cam;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gc = FindObjectOfType<GameController>();
        cam = Camera.main;
        screenWidthAndOffset = cam.aspect * cam.orthographicSize + 2f;
        stopPosX = cam.aspect * cam.orthographicSize - 2f;
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
            StartCoroutine(Warning());
        }

        if(transform.position.y > cam.orthographicSize + 1)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Warning()
    {
        sparks.SetActive(true);
        yield return new WaitForSeconds(1);
        sparks.SetActive(false);
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        float timer = 1f;
        laser.SetActive(true);
        audioSource.Play();
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.left, 25f, damageLayer);
            Debug.DrawRay(transform.position, Vector3.left * 25f, Color.red);
            if (hit)
            {
                if(hit.transform.tag == "DamageTrigger")
                {
                    gc.playerDead = true;
                }
            }

            yield return null;
        }
        laser.SetActive(false);
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
        StartCoroutine(Warning());
    }
}
