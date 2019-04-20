using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]
    private GameObject starExplode;
    private GameController gc;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gc = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Alien")
        {
            gc.EnemyDeath();
            gc.StarCount += 4;
            Instantiate(starExplode, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}
