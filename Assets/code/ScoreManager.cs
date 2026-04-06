using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    
    public static ScoreManager Instance;    //call with ScoreManager.Instance
    public int running_score;
    public int final_score;
    public int num_notes;
    public int current_streak;
    public int longest_streak;

    void Awake ()
    {
        Instance = this;
    }

    void Update ()
    {
        if (RhythmManager.Instance.hasStarted && Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Your final score is: " + CalcFinalScore() + "/100");
            Debug.Log("Your longest streak was: " + GetLongestStreak() + "/" + num_notes + " notes");
            
        }
    }

    public void StartPerformance()
    {
        running_score = 0;
        final_score = 0;
        num_notes = 0;
        current_streak = 0;
        longest_streak = 0;
    }

    public void AddScore(int amount)
    {
        running_score = running_score + amount;
    }

    public void SubScore(int amount)
    {
        running_score = running_score - amount;
    }

    //this function will be used by the note spawner function to calculate what the final score will be
    public void AddNote()
    {
        num_notes++;
    }

    public void AddToStreak()
    {
        current_streak++;
        if (current_streak >= longest_streak) longest_streak = current_streak;
    }

    public void BreakStreak()
    {
        current_streak = 0;
    }

    public int GetCurrentStreak()
    {
        return current_streak;
    }

    public int GetLongestStreak()
    {
        return longest_streak;
    }

    //once all the notes have been played/accounted for/the performance is done, run this to find attack power
    public int CalcFinalScore()
    {
        final_score = running_score/num_notes;

        //bounds for the score/out of 100
        if (final_score < 0)
        {
            final_score = 0;
        }
        /*if (final_score > 100)
        {
            final_score = 100;
        }*/

        return final_score;
    }
}