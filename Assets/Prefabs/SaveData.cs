using UnityEngine;

[CreateAssetMenu(fileName = "SaveData", menuName = "Data/SaveData")]
public class SaveData : ScriptableObject
{
    [Header("Position")]
    public float x;
    public float y;

    [Header("Level")]
    public int level;
    public int experience;

    [Header("Stats")]
    public int attack;
    public int defense;
    public int maxHealth;

    //eventually add states for bosses defeated
}
