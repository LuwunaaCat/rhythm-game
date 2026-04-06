using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyEffectType{
        None,
        Normalize,   //No critical attacks at 100% power
        Blindness,   //part of the note highway is obscured
        Devalor,     //Notes are worth less than normal
        //FallingFlat,    //Attacks fail if the power is too low
        ToneDeaf,   //Disable certain audio tracks
        Precision,   //Smaller timing for perfect/nice notes
        Accidental, //Send out fake notes that penalize for being hit
    }

    public MoveData[] moves;

    [Header("Identity")]
    public string enemyName;
    public Sprite sprite;

    [Header("Stats")]
    public float currentHealth;
    public int maxHealth;
    public int attack;
    public int defense;
    public int level;

    [Header("Drops")]
    public int currency;
    public int expGranted;

    [Header("Special Effects")]
    public EnemyEffectType specialEffect;

    void Start()
    {
        currentHealth = (float)maxHealth;
        sprite = GetComponent<Sprite>();
    }
}