using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private float baseSpeed;
    [SerializeField]
    private float baseSpawnTimer;
    [SerializeField]
    private int baseChangeTimer;
    [SerializeField]
    private int adjustAmount;
    [SerializeField]
    private TextMeshProUGUI starText;
    [SerializeField]
    private GameObject pauseMenu;

    private float speed;
    private float spawnTimer;
    private int changeTimer;
    private int starCount;
    private bool paused;
    private bool gameStarted;

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public float SpawnTimer
    {
        get { return spawnTimer; }
        set { spawnTimer = value; }
    }

    public int StarCount
    {
        get { return starCount; }
        set { starCount = value; }
    }

    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        starText.text = "= " + starCount.ToString("000000");
        GameAdjust();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Paused();
        }
    }

    void GameAdjust()
    {
        if (Time.time > changeTimer)
        {
            changeTimer += adjustAmount;
            float adjust = spawnTimer * .2f;
            float speedAdj = speed + .5f;

            if (spawnTimer - adjust <= .5f)
            {
                spawnTimer = .5f;
            }
            else
            {
                spawnTimer -= adjust;
            }

            if (speed > 10f)
            {
                speed = 10f;
            }
            else
            {
                speed = speedAdj;
            }
        }
    }

    void Paused()
    {
        paused = !paused;
        if (paused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    private void Reset()
    {
        speed = baseSpeed;
        spawnTimer = baseSpawnTimer;
        changeTimer = baseChangeTimer;
    }

    public void Retry()
    {
        Paused();
        Reset();
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Paused();
        Reset();
        SceneManager.LoadScene(0);
    }
}
