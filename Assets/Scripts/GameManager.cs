using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverMenu;
    public GameObject audioPlayer;
    public AudioClip powerUpSound;
    public AudioClip powerDownSound;
    //public GameObject enemyOnePrefab;
    public GameObject[] enemyPrefabs;
    public GameObject cloudPrefab;
    public GameObject powerUpPrefab;
    public GameObject coinPrefab;
    public TextMeshProUGUI powerUpText;
    public TextMeshProUGUI scoreText;


    public float horizontalScreenSize;
    public float verticalScreenSize;

    public TextMeshProUGUI livesText;
    public int score;

    private bool gameOver;

    public float powerUpDespawnTime = 4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;
        gameOver = false;
        CreateSky();
        score = 0;

        InvokeRepeating("CreateEnemyOne", 1f, 2f);
        InvokeRepeating("CreateEnemyTwo", 5f, 4f);

        StartCoroutine(SpawnPowerUp());
        StartCoroutine(SpawnCoin());

        powerUpText.text = "No Power-Up";
    }

    private void Update()
    {
        if(gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    IEnumerator SpawnPowerUp()
    {
        float spawnTime = Random.Range(3, 5);
        yield return new WaitForSeconds(spawnTime);
        CreatePowerUp();
        StartCoroutine(SpawnPowerUp());
    }

    IEnumerator SpawnCoin()
    {
        float spawnTime = Random.Range(1, 7);
        yield return new WaitForSeconds(spawnTime);
        CreateCoin();
        StartCoroutine(SpawnCoin());
    }


    public void ManagePowerupText(int powerUpType)
    {
        switch (powerUpType)
        {
            case 1:
                powerUpText.text = "Speed!";
                break;
            case 2:
                powerUpText.text = "Double Weapons!";
                break;
            case 3:
                powerUpText.text = "Triple Weapon!";
                break;
            case 4:
                powerUpText.text = "Shield!";
                break;
            default:
                powerUpText.text = "No Power-Up";
                break;


        }
    }

    public void PlaySound(int whichSound)
    {
        switch (whichSound)
        {
            case 1:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerUpSound);
                break;
            case 2:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerDownSound);
                break;
        }
    }

    void CreatePowerUp()
    {
        GameObject powerUp = Instantiate(powerUpPrefab, new Vector3(Random.Range(-horizontalScreenSize * .8f, horizontalScreenSize * .8f), Random.Range(-verticalScreenSize *.8f, 0), 0), Quaternion.identity);
        Destroy(powerUp, powerUpDespawnTime);
    }
    void CreateCoin()
    {
        GameObject coin = Instantiate(coinPrefab, new Vector3(Random.Range(-horizontalScreenSize * .8f, horizontalScreenSize * .8f), Random.Range(-verticalScreenSize * .8f, 0), 0), Quaternion.identity);
        Destroy(coin, powerUpDespawnTime);
    }


    void CreateEnemyOne()
    {
        Instantiate(enemyPrefabs[0], new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), verticalScreenSize, 0), Quaternion.identity);
    }
    void CreateEnemyTwo()
    {
        Instantiate(enemyPrefabs[1], new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), verticalScreenSize, 0), Quaternion.identity);
    }
    void CreateSky()
    {
        for(int i = 0; i< 30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }
    }

    public void AddScore(int earnedScore)
    {
        score = score + earnedScore;
        ChangeScoreText(score);
    }
    void ChangeScoreText(int score)
    {
        scoreText.text = "Score:" + score;
    }

    public void ChangeLivesText (int currentLives)
    {
        livesText.text = "lives: " + currentLives;
    }

    public void GameOver()
    {
        //set game over object menu to true
        gameOverMenu.SetActive(true);
        //game over to be true
        gameOver = true;
    }
}
