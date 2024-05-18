using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public TextMeshProUGUI narrativeText;
    private string[] narratives = {
        "Every day, thousands of tons of e-waste are dumped into the ocean.",
        "This pollution devastates ecosystems and threatens marine life.",
        "But the fight against e-waste pollution is not over. It takes all of us to protect our oceans."
    };
    private int currentNarrativeIndex = 0;
    private float fadeInDuration = 1f;
    private float displayDuration = 5f;

    void Start()
    {
        StartCoroutine(DisplayNarratives());
    }

    IEnumerator DisplayNarratives()
    {
        foreach (string narrative in narratives)
        {
            // Fade in the narrative text
            yield return FadeInText(narrative, fadeInDuration);
            
            // Wait for the display duration
            yield return new WaitForSeconds(displayDuration);
            
            // Fade out the narrative text
            yield return FadeOutText(fadeInDuration);
            
            // Increment the narrative index
            currentNarrativeIndex++;
        }

        // Load the EndDay scene
        SceneManager.LoadScene(5);
    }

    IEnumerator FadeInText(string text, float duration)
    {
        float elapsedTime = 0f;
        narrativeText.text = text;
        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            narrativeText.color = new Color(narrativeText.color.r, narrativeText.color.g, narrativeText.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        narrativeText.color = new Color(narrativeText.color.r, narrativeText.color.g, narrativeText.color.b, 1f);
    }

    IEnumerator FadeOutText(float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            narrativeText.color = new Color(narrativeText.color.r, narrativeText.color.g, narrativeText.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        narrativeText.color = new Color(narrativeText.color.r, narrativeText.color.g, narrativeText.color.b, 0f);
    }

    public void RestartGame()
    {
        // Restart the game by reloading the MainGame scene
        SceneManager.LoadScene("MainGame");
    }
}
