using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] alienPrefabs;

    private float spawnTimer;

    private GameController gc;

    private void Start()
    {
        gc = FindObjectOfType<GameController>();
        StartCoroutine(SpawnAliens());
    }

    private void Update()
    {
        spawnTimer = gc.SpawnTimer;
    }

    IEnumerator SpawnAliens()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTimer);

            int alienIndex = Random.Range(0, alienPrefabs.Length);
            Instantiate(alienPrefabs[alienIndex], transform.position, Quaternion.identity);
        }
    }
}
