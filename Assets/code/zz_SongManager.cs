using UnityEngine;
using System;
using System.IO;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance;
    public float tempo;
    public int timeSig;
    public float speed;
    public float perfThreshold;
    public float niceThreshold;

    void Awake()
    {
        Instance = this;
        speed = 2;
        perfThreshold = 0.4f;
        niceThreshold = 0.75f;
        ReadSongFile();
    }

    private void ReadSongFile()
    {
        string path = Application.streamingAssetsPath + "/never_gonna.txt";
        string[] lines = File.ReadAllLines(path);
        tempo = float.Parse(lines[0].Trim());
        timeSig = int.Parse(lines[1].Trim());
        Array.Clear(lines, 0, lines.Length);
    }
}