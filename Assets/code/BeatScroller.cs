using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float tempo;
    public int time_sig;
    private float scrollSpeed;

    void Start()
    {
        /* tempo = SongManager.Instance.tempo;
        time_sig = SongManager.Instance.timeSig;
        game_speed = SongManager.Instance.speed; */

        //distance / time (time signature * time duration of beats * game speed)
        //9 is the distance from the top to the bottom of the screen, minus the distance from the bottom to the buttons/2 (10-1/2)
        scrollSpeed = 9.67f / (time_sig * (60/tempo));
    }

    void Update()
    {
        if (RhythmManager.Instance.hasStarted)
        {
            transform.position -= new Vector3(0, Time.deltaTime * scrollSpeed, 0);            
        }
    }
}