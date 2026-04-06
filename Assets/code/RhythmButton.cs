using UnityEngine;
using System;
using System.Collections.Generic;

public class RhythmButton : MonoBehaviour
{
    //changing sprite
    private SpriteRenderer SR;
    private Collider2D Hitbox;
    public Sprite defaultImage;
    public Sprite pressedImage;
    public KeyCode keyToPress; 

    //calculating score
    private Queue<Note> noteQueue = new Queue<Note>();
    private Note currentNote;
    private float perfThreshold = 0.4f;
    private float niceThreshold = 0.75f;
    private float perfToNiceDist = 0.35f;
    private float dist;
    

    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        Hitbox = GetComponent<Collider2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            SR.sprite = pressedImage;
        }
        if (Input.GetKeyUp(keyToPress))
        {
            SR.sprite = defaultImage;
        }
        //make sure to dequeue and destroy the note once it is hit!!

        if(Input.GetKeyDown(keyToPress) && noteQueue.Count > 0){
            currentNote = noteQueue.Peek();
            if (currentNote.laneKey == keyToPress){
                //calculates distance from the center of the hitbox to the note position
                dist = MathF.Abs(currentNote.transform.position.y - transform.position.y);         
                if (dist <= perfThreshold)
                {
                    Debug.Log("Perfect note! Distance to center:" + dist + "Score granted = 100");
                    //TextHits.Instance.printPerfectHit();
                    ScoreManager.Instance.AddScore(100);
                    ScoreManager.Instance.AddToStreak();
                    currentNote.isHit = true;
                    noteQueue.Dequeue();
                    currentNote.DestroyNote();
                    currentNote = null;
                }
                else if (dist <= niceThreshold)
                {
                    int bonus = (int)(100*((1/perfToNiceDist)*(perfToNiceDist-(Mathf.Abs(dist)-perfThreshold))));
                    /*Example margins: Perfect = 0.2f, Nice = 0.5f
                      atp distance = .2 < x <= .5
                      dist_bonus = 0-100 (aka 0.5 = 0, 2.0 = 100, etc); */
                    Debug.Log("Nice note! Distance to center:" + dist + "Score granted = " + bonus);
                    //TextHits.Instance.printNiceHit();
                    ScoreManager.Instance.AddScore(bonus);
                    ScoreManager.Instance.AddToStreak();
                    currentNote.isHit = true;
                    noteQueue.Dequeue();
                    currentNote.DestroyNote();
                    currentNote = null;
                }
                else //(dist > niceThreshold)
                {
                    Debug.Log("Bad note! Distance to center:" + dist + "Score subtracted 50");
                    //TextHits.Instance.printMissHit();
                    ScoreManager.Instance.SubScore(50);
                    ScoreManager.Instance.BreakStreak();
                    currentNote.DestroyNote();
                    currentNote.isHit = true;
                    noteQueue.Dequeue();
                    currentNote.DestroyNote();
                    currentNote = null;
                }
            }
        }   
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Note"))  return;
        ScoreManager.Instance.AddNote();
        
        //i'm not sure why I need to use this syntax, maybe type casting a Collider2D to a Note?
        currentNote = other.GetComponent<Note>();
        currentNote.isHit = false;
        noteQueue.Enqueue(currentNote);
        currentNote = null;
        //Debug.Log("Note entered hitbox");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        currentNote = other.GetComponent<Note>();
        if (!other.CompareTag("Note") || noteQueue.Count <= 0) return;

        if (currentNote.isHit == false) {
            //if the note has left the stage, clear the reference
            Debug.Log("Note missed! Score subtracted 50");
            //TextHits.Instance.printMissHit();
            ScoreManager.Instance.SubScore(50);
            ScoreManager.Instance.BreakStreak();
            noteQueue.Dequeue();
            currentNote = null;  
            //Debug.Log("Note exited hitbox");
        }
    }
}