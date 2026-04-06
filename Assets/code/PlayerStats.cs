using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    public string playerName;
    public float currentHealth;
    public int maxHealth = 12;
    public int experience;
    public int level = 1;
    public int attack = 10;
    public int defense = 10;
    public MoveData[] moves;

    void Awake ()
    {
        Instance = this;
    }
    void Start()
    {
        currentHealth = (float)maxHealth;
    }

    public void damagePlayer(int health)
    {
        if (currentHealth - health > 0) {
            currentHealth = currentHealth - health;
            Debug.Log("Player took " + health + " damage!");
        }
        else
        {
            currentHealth = 0;
            Debug.Log("Player fainted!");
        }
    }

    public void healPlayer(int health)
    {
        if (currentHealth + health <= maxHealth)
        {
            currentHealth = currentHealth + health;
            Debug.Log("Player healed " + health + " HP!");
        }
        else
        {
            currentHealth = maxHealth;
            Debug.Log("Player's health is now full!");
        }
    }

    public void grantEXP(int expGained)
    {
        experience = experience + expGained;
    }

    public float getXPos()
    {
        return transform.position.x;
    }

    public float getYPos()
    {
        return transform.position.y;
    }

}
