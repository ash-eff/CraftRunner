using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject starPrefab;
    [SerializeField]
    private float minStars;
    [SerializeField]
    private float maxStars;
    [SerializeField]
    private float minTimeBetweenSpawn;
    [SerializeField]
    private float maxTimeBetweenSpawn;

    public float timeBetweenItems;
    private float screenWidth;

    private bool spawning = true;

    private GameController gc;
    private Camera cam;

    private void Awake()
    {
        gc = FindObjectOfType<GameController>();
        cam = Camera.main;
        screenWidth = cam.aspect * cam.orthographicSize + 1;
    }

    private void Update()
    {
        screenWidth = cam.aspect * cam.orthographicSize + 1;
        timeBetweenItems = gc.Speed / (gc.Speed * 4f);
        if (spawning && !gc.gameOver)
        {
            spawning = false;
            StartCoroutine(SpawnStars());
        }
    }

    IEnumerator SpawnStars()
    {
        float randNum = Random.Range(minStars, maxStars);
        float randHeight = Random.value;
        float height = 0f;
        if(randHeight > .66f)
        {
            height = 4f;
        }
        else if(randHeight > .33f)
        {
            height = 1f;
        }
        else
        {
            height = -4f;
        }

        for(int i = 0; i < randNum; i++)
        {
            Instantiate(starPrefab, new Vector2(screenWidth , height), Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenItems);
        }

        float randSpawnTime = Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
        yield return new WaitForSeconds(randSpawnTime);
        spawning = true;
    }
}
