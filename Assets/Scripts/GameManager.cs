using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public float gameDuration = 120f; // Game duration in seconds
    public GameObject[] fishPrefabs; // Array of fish prefabs
    public GameObject[] trashPrefabs; // Array of trash prefabs
    public Transform skyBoundary;
    public int numberOfDays = 3; // Number of days to play

    private int currentScore = 0;
    private float timeRemaining;
    private int currentDay;

    void Start()
    {
        currentDay = PlayerPrefs.GetInt("Day", 1);

        timeRemaining = gameDuration;
        UpdateScoreText();
        UpdateTimerText();
        StartNewDay();
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerText();

            if (timeRemaining <= 0)
            {
                EndDay();
            }
        }
    }

    void StartNewDay()
    {
        // Spawn fish and trash objects for the current day
        SpawnFishObjects();
        SpawnTrashObjects();

        // Reset timer for the new day
        timeRemaining = gameDuration;
        UpdateTimerText();
    }

    void SpawnFishObjects()
    {
        int minFishCount = 0;
        int maxFishCount = 0;

        // Adjust the range based on the current day
        switch (currentDay)
        {
            case 1:
                minFishCount = 8;
                maxFishCount = 12;
                break;
            case 2:
                minFishCount = 5;
                maxFishCount = 6;
                break;
            case 3:
                minFishCount = 2;
                maxFishCount = 3;
                break;
            default:
                break;
        }

        int numberOfFish = Random.Range(minFishCount, maxFishCount + 1); // Add 1 to include the max value
        for (int i = 0; i < numberOfFish; i++)
        {
            // Randomly select a fish prefab
            GameObject fishPrefab = fishPrefabs[Random.Range(0, fishPrefabs.Length)];
            // Instantiate the fish
            GameObject fish = Instantiate(fishPrefab, GetRandomSpawnPosition(-5f, -17f), Quaternion.identity);
            // Set fish as child of skyBoundary
            fish.transform.SetParent(skyBoundary);
        }
    }

    void SpawnTrashObjects()
    {
        int minTrashCount = 0;
        int maxTrashCount = 0;

        // Adjust the range based on the current day
        switch (currentDay)
        {
            case 1:
                minTrashCount = 2;
                maxTrashCount = 3;
                break;
            case 2:
                minTrashCount = 5;
                maxTrashCount = 6;
                break;
            case 3:
                minTrashCount = 8;
                maxTrashCount = 12;
                break;
            default:
                break;
        }

        int numberOfTrash = Random.Range(minTrashCount, maxTrashCount + 1); // Add 1 to include the max value
        for (int i = 0; i < numberOfTrash; i++)
        {
            // Randomly select a trash prefab
            GameObject trashPrefab = trashPrefabs[Random.Range(0, trashPrefabs.Length)];
            // Instantiate the trash
            GameObject trash = Instantiate(trashPrefab, GetRandomSpawnPosition(-15f, -16f), Quaternion.identity);
            // Set trash as child of skyBoundary
            trash.transform.SetParent(skyBoundary);
        }
    }


    Vector3 GetRandomSpawnPosition(float maxY, float minY)
    {
        float minX = skyBoundary.position.x - (skyBoundary.localScale.x / 2);
        float maxX = skyBoundary.position.x + (skyBoundary.localScale.x / 2);
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        return new Vector3(randomX, randomY, 0f);
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + currentScore.ToString();
    }

    void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();
    }

    void EndDay()
    {
        PlayerPrefs.SetInt("Day", currentDay);
        PlayerPrefs.SetInt("Score", currentScore);
        
        currentDay++;
        if (currentDay > numberOfDays)
        {
            EndGame();
        }
        else
        {
            SceneManager.LoadScene(3);
        }
    }

    void EndGame()
    {
        SceneManager.LoadScene(4);
    }
}

