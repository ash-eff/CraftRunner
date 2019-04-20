using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollector : MonoBehaviour
{
    AudioSource audioSource;
    GameController gc;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gc = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Star")
        {
            audioSource.Play();
            gc.StarCount++;
            Destroy(collision.gameObject);
        }
    }
}
