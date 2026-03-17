using TMPro;
using UnityEngine;

public class ScoreOnDeath : MonoBehaviour
{
    public string scoreTextObjectName = "ScoreText";
    public int points = 10;

    private static TMP_Text scoreText;
    private static int score;
    private static bool initialized;

    private void Awake()
    {
        if (scoreText == null)
        {
            GameObject obj = GameObject.Find(scoreTextObjectName);
            if (obj != null) scoreText = obj.GetComponent<TMP_Text>();
        }

        if (!initialized && scoreText != null)
        {
            initialized = true;
            score = ParseScore(scoreText.text);
            UpdateText();
        }
    }

    private void OnDestroy()
    {
        if (scoreText == null) return;

        score += points;
        UpdateText();
    }

    public static void ResetScore()
    {
        score = 0;
        initialized = false;

        if (scoreText != null)
        {
            scoreText.text = "Score: 0";
        }
    }

    private void UpdateText()
    {
        string t = scoreText.text ?? "";
        int idx = t.LastIndexOf(':');

        if (idx >= 0)
        {
            string left = t.Substring(0, idx).TrimEnd();
            if (string.IsNullOrEmpty(left)) left = "Score";
            scoreText.text = left + ": " + score;
        }
        else
        {
            scoreText.text = "Score: " + score;
        }
    }

    private int ParseScore(string text)
    {
        if (string.IsNullOrEmpty(text)) return 0;

        int idx = text.LastIndexOf(':');
        string right = idx >= 0 ? text.Substring(idx + 1) : text;

        int.TryParse(right.Trim(), out int v);
        return v;
    }
}