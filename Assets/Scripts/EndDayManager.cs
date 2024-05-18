using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndDayManager : MonoBehaviour
{
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI dayScoreText;
    public TextMeshProUGUI totalScoreText;

    void Start()
    {
        // Get the day number from PlayerPrefs
        int currentDay = PlayerPrefs.GetInt("Day", 1);

        // Get the score for the current day from PlayerPrefs
        int dayScore = PlayerPrefs.GetInt("Score", 0);

        // Display the day number
        dayText.text = "Day: " + currentDay.ToString();

        PlayerPrefs.SetInt("Day", currentDay + 1);

        // Display the score for the current day
        dayScoreText.text = "Day Score: " + dayScore.ToString();

        // Calculate the total score for all days
        int totalScore = PlayerPrefs.GetInt("TotalScore", 0); // Get the current total score from PlayerPrefs

        // Add the current day score to the total score
        totalScore += dayScore;

        // Save the updated total score back to PlayerPrefs
        PlayerPrefs.SetInt("TotalScore", totalScore);

        // Display the total score
        totalScoreText.text = "Total Score: " + totalScore.ToString();
    }
}

