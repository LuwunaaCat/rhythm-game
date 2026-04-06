using UnityEngine;
using TMPro;

public class TextHits : MonoBehaviour
{
    public TMP_Text scoreText;
    public static TextHits Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }

    public void printNiceHit()
    {
        scoreText.text = "Nice Hit!";
    }

    public void printPerfectHit()
    {
        scoreText.text = "Perfect Hit!";
    }

    public void printMissHit()
    {
        scoreText.text = "Miss!";
    }

    public void printScore()
    {
        int final_score = ScoreManager.Instance.CalcFinalScore();
        string final_score_str = final_score.ToString();
        scoreText.text = final_score_str + "% power!";
    }
}