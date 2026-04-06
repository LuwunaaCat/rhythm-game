using UnityEngine;

public class RhythmManagerOld : MonoBehaviour
{
    //public static RhythmManagerOld Instance;
    public GameObject Buttons;
    public GameObject NoteHighway;
    public Canvas tempCanvas;
    public BeatScroller BS;
    public AudioSource Music;
    void Awake()
    {
        //Instance = this;
    }

    void Start()
    {
        hideRhythm();
    }

    
    public void showRhythm()
    {
        Buttons.SetActive(true);
        NoteHighway.SetActive(true);
        tempCanvas.enabled = true;
    }

    public void enableRhythm()
    {
        //BS.hasStarted = true;
        Music.Play();
    }

    public void disableRhythm()
    {
        //BS.hasStarted = false;
        ScoreManager.Instance.CalcFinalScore();
        TextHits.Instance.printScore();
    }

    public void hideRhythm()
    {
        Buttons.SetActive(false);
        NoteHighway.SetActive(false);
        tempCanvas.enabled = false;
    }
}
