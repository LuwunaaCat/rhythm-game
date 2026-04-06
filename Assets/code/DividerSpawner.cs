using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public class DividerSpawner : MonoBehaviour
{
    private int currentBeat;
    private int lastBeat;
    private float secondsPerBeat, beatsPerSecond;
    private float repeatStart, repeatEnd;
    private float songTime;
    private float loopWarning;
    [SerializeField] private GameObject BeatDivider;
    [SerializeField] private BeatScroller BS;
    float interval; // time between dividers in seconds
    float nextSpawnTime;

    void Start()
    {        
        interval = 60f/NoteSpawner.Instance.tempo;
        nextSpawnTime = interval;
    }

    void Update()
    {
        //if the time is a whole number/had no remainder (on the beat) 
        if (NoteSpawner.Instance.songTime >= nextSpawnTime)
        {
            nextSpawnTime = nextSpawnTime + interval;
            //spawn a divider!
            GameObject newDivider;
            //select which note to spawn
            newDivider = Instantiate(BeatDivider, new Vector3(500, 6, 0), Quaternion.identity, BS.transform);
        }
        if (RhythmManager.Instance.rhythmTime() <= NoteSpawner.Instance.repeatStartSec + (secondsPerBeat * 0.5f) && 
                RhythmManager.Instance.rhythmTime() >= NoteSpawner.Instance.repeatStartSec - (secondsPerBeat * 0.5f))
        {
            nextSpawnTime = NoteSpawner.Instance.repeatStartSec;
        }
    }
}