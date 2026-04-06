using UnityEngine;

[CreateAssetMenu(fileName = "MoveData", menuName = "Battle/Move")]
public class MoveData : ScriptableObject
{
    public enum MoveType {ATTACK, PROTECT, ATTACK_UP, DEFENSE_UP};
    public enum HitRequirement {ACCURACY, COMBO};
    public enum StatusCondition {NONE, DEAFENED};
    public int id;
    public string moveName;
    public string description;
    public float damage;
    public MoveType type;
    public HitRequirement requirement;
    public int threshold;
    public StatusCondition status;
}