using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public class NoteSpawner : MonoBehaviour
{
    //create a dynamic array, List, of notedatas to store all of the notes
    private List<NoteData> notes = new List<NoteData>();
    private float spawnTime;
    private char lane;
    private int noteIndex = 0;
    [Header("Song Data")]
    public float tempo;
    public int timeSig;
    public float songTime;
    public bool aboutToRepeat = false;
    public bool hasRepeated = false;
    public float repeatStartSec, repeatEndSec;   //parsed and calculated from txt file
    public float loopWarningSec;
    private float secondsPerBeat;
    private float beatsPerSecond;
    public float timeForPerformance;
    [Header("Notes")]
    [SerializeField] private BeatScroller BS;
    [SerializeField] private GameObject NoteD;
    [SerializeField] private GameObject NoteF;
    [SerializeField] private GameObject NoteJ;
    [SerializeField] private GameObject NoteK;
    [SerializeField] private GameObject Deactive_NoteD;
    [SerializeField] private GameObject Deactive_NoteF;
    [SerializeField] private GameObject Deactive_NoteJ;
    [SerializeField] private GameObject Deactive_NoteK;
    private string path;
    private string[] lines;
    public static NoteSpawner Instance; //used for tempo and timesig in different scripts

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {           
        path = Application.streamingAssetsPath + "/never_gonna.txt";
        lines = File.ReadAllLines(path);
        repeatStartSec = (float.Parse(lines[0].Trim()) +1f) * (60f/tempo);
        repeatEndSec = (float.Parse(lines[1].Trim()) +1f) * (60f/tempo);
        tempo = float.Parse(lines[2].Trim());
        timeSig = int.Parse(lines[3].Trim());
            //for loop that passes in one line at a time starting after tempo & time signature
        for (int i = 4; i < lines.Length; i++)
        {
            //Debug.Log("Reading " + lines[i]);
            if (lines[i][0] == '#')
            {
                continue;
            }
                //split the line at the comma
            string[] parts = lines[i].Split(',');
                //parse the first half as a float
            spawnTime = float.Parse(parts[0].Trim());
                //assign the second half (thats already a string)
            lane = char.Parse(parts[1].Trim());
                //create a new NoteData with the new info
            NoteData newNote = new NoteData(spawnTime, lane);
                //append newNote to the end of our list of NoteData's "notes"
            notes.Add(newNote);
        }
        //Debug.Log(repeatStartSec + " " + repeatEndSec + " " + tempo + " " + timeSig);
        secondsPerBeat = 60f/tempo;
        beatsPerSecond = tempo / 60f;
        //the time in seconds of one measure before we repeat
        loopWarningSec = repeatEndSec - (timeSig * secondsPerBeat);
    }

    void Update()
    {
        if(!aboutToRepeat) {
            songTime = RhythmManager.Instance.rhythmTime();
            if (songTime >= loopWarningSec)
            {
                aboutToRepeat = true;
                noteIndex = 0;
            }
        }
        else {
            //if we are a measure away from repeating...
            //songTime = (repeatStart - one measure) + (the real song time - one measure before looping)
            songTime = (repeatStartSec - (timeSig * secondsPerBeat)) + (RhythmManager.Instance.rhythmTime() - loopWarningSec);

            //if real time is within half a second of repeatStart
            if (RhythmManager.Instance.rhythmTime() <= repeatStartSec + (secondsPerBeat * 0.5f) && 
                RhythmManager.Instance.rhythmTime() >= repeatStartSec - (secondsPerBeat * 0.5f))
            {
                aboutToRepeat = false;
                hasRepeated = true;
            }
        }

        //Debug.Log(songTime);

            //if there are still notes to play && the time of the song matches with the time for the note to spawn 
        if (noteIndex < notes.Count && (beatsPerSecond*songTime)+1f >= (notes[noteIndex].spawnTime-timeSig))
        {
            //spawn a note!
            GameObject newNote;

            if (BattleManager.Instance.readyToAttack){
                //select which note to spawn
                switch (notes[noteIndex].lane)
                {
                    case 'D':  
                        newNote = Instantiate(NoteD, new Vector3(498.5f, 6, 0), Quaternion.identity, BS.transform);
                        break;
                    case 'F': 
                        newNote = Instantiate(NoteF, new Vector3(499.5f, 6, 0), Quaternion.identity, BS.transform);
                        break;
                    case 'J': 
                        newNote = Instantiate(NoteJ, new Vector3(500.5f, 6, 0), Quaternion.identity, BS.transform);
                        break;
                    case 'K': 
                        newNote = Instantiate(NoteK, new Vector3(501.5f, 6, 0), Quaternion.identity, BS.transform);
                        break;
                    default: return; // unknown lane, do nothing
                }
            }
            else
            {
                switch (notes[noteIndex].lane)
                {
                    case 'D':  
                        newNote = Instantiate(Deactive_NoteD, new Vector3(498.5f, 6, 0), Quaternion.identity, BS.transform);
                        break;
                    case 'F': 
                        newNote = Instantiate(Deactive_NoteF, new Vector3(499.5f, 6, 0), Quaternion.identity, BS.transform);
                        break;
                    case 'J': 
                        newNote = Instantiate(Deactive_NoteJ, new Vector3(500.5f, 6, 0), Quaternion.identity, BS.transform);
                        break;
                    case 'K': 
                        newNote = Instantiate(Deactive_NoteK, new Vector3(501.5f, 6, 0), Quaternion.identity, BS.transform);
                        break;
                    default: return; // unknown lane, do nothing
                }
            }
            noteIndex++;
        }
    }

    public class NoteData
    {
        public float spawnTime;
        public char lane;

            //constructor, creates a NoteData w/ attributes spawnTime and lane
        public NoteData(float spawnTime, char lane)
        {
            this.spawnTime = spawnTime;
            this.lane = lane;
        }
    }   
}