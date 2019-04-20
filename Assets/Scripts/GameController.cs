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
    private TextMeshProUGUI finalStarText;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private GameObject deathAnim;
    [SerializeField]
    private Animator tally;
    [SerializeField]
    private AudioClip deathAudio;
    [SerializeField]
    private AudioClip boomAudio;
    [SerializeField]
    private AudioClip starAudio;

    AudioSource audioSource;

    public float speed;
    public float spawnTimer;
    public int changeTimer;
    private int starCount;
    private bool paused;
    private bool gameStarted;

    public bool playerDead;
    public bool gameOver;

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
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1f;
        GameReset();
    }

    private void Update()
    {
        starText.text = "= " + starCount.ToString("000000");
        GameAdjust();

        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
            Paused();
        }

        if(playerDead && !gameOver)
        {
            gameOver = true;
            StartCoroutine(GameOverScreen());
        }
    }

    IEnumerator GameOverScreen()
    {
        Player player = FindObjectOfType<Player>();
        player.gameObject.SetActive(false);
        audioSource.PlayOneShot(deathAudio);
        GameObject deathAnimation = Instantiate(deathAnim, player.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(.5f);

        float timescale = 1f;
        
        while(timescale > 0.01f)
        {
            timescale -= Time.deltaTime;
            Time.timeScale = timescale;
        
            yield return null;
        }
        
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
        StartCoroutine(TallyPoints());
    }

    IEnumerator TallyPoints()
    {
        int tempScore = 0;
        tally.gameObject.SetActive(true);
        audioSource.volume = .15f;
        while(tempScore < starCount)
        {
            audioSource.PlayOneShot(starAudio);
            tempScore++;
            finalStarText.text = "Score: " + tempScore.ToString("000000");
            yield return new WaitForSecondsRealtime(.05f);
        }

        audioSource.volume = 1f;
        tally.gameObject.SetActive(false);
        Debug.Log("Done");
    }

    public void EnemyDeath()
    {
        audioSource.PlayOneShot(boomAudio);
    }

    void GameAdjust()
    {
        if (Time.timeSinceLevelLoad > changeTimer)
        {
            changeTimer += adjustAmount;
            float adjust = spawnTimer * .1f;
            float speedAdj = speed + .5f;

            if (spawnTimer - adjust <= 2f)
            {
                spawnTimer = 2f;
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

    private void GameReset()
    {
        Time.timeScale = 1;
        speed = baseSpeed;
        spawnTimer = baseSpawnTimer;
        changeTimer = baseChangeTimer;
        playerDead = false;
        gameOver = false;
    }

    public void Retry()
    {
        Paused();
        GameReset();
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Paused();
        GameReset();
        SceneManager.LoadScene(0);
    }
}
