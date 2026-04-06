using UnityEngine;

[CreateAssetMenu(fileName = "BoonData", menuName = "Battle/Boon")]
public class Boon : ScriptableObject
{
    public enum Rarity {Common, Uncommon, Rare, Legendary, Mythical};
    public int ID;
    public Sprite sprite;
    public string boonName;
    public string descriptionLong;
    public string descriptionShort;
    public Rarity rarity;
    public bool canUpgrade;
}
