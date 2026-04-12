using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class GameManager : MonoBehaviour
{
    //public GameObject enemyOnePrefab;
    public GameObject[] enemyPrefabs;
    public GameObject cloudPrefab;

    public float horizontalScreenSize;
    public float verticalScreenSize;

    public TextMeshProUGUI livesText;
    public int score;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;

        CreateSky();
        score = 0;

        InvokeRepeating("CreateEnemyOne", 1f, 2f);
        InvokeRepeating("CreateEnemyTwo", 5f, 4f);

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
    }

    public void ChangeLivesText (int currentLives)
    {
        livesText.text = "lives: " + currentLives;
    }
}
