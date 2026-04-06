using UnityEngine;
using TMPro;
using System;

public class BattleManager : MonoBehaviour
{
    public enum Item {HEALING};
    public enum battleResult {LOSS, WIN};
    public enum battleState {SELECTING, ATTACKING};
    public static BattleManager Instance;

    public battleState state;
    public GameObject rhythmSection;
    public Camera battleCamera;
    public GameObject DefaultButtons;
    public GameObject MoveButtons;
    public bool readyToAttack;

    [Header("Button Text")]
    public TMP_Text buttonText1;
    public TMP_Text buttonText2;
    public TMP_Text buttonText3;
    public TMP_Text buttonText4;

    [Header("Combatant Text")]
    public TMP_Text playerName;
    public TMP_Text enemyName;
    public TMP_Text playerHP;
    public TMP_Text enemyHP;


    private Enemy enemy;
    private MoveData move;
    private float playerAttackStat;
    private float playerDefenseStat;
    private int playerMaxHealth;
    private int playerCurrentHealth;
    private float enemyAttackStat;
    private float enemyDefenseStat;
    private int enemyMaxHealth;
    private int enemyCurrentHealth;
    void Awake()
    {
        battleCamera.enabled = false;
        Instance = this;
    }

    void Update()
    {
        if (GameManager.Instance.getState() == GameManager.State.Battling && readyToAttack == false)
        {
            switch (move.type)
            {
                case MoveData.MoveType.ATTACK:
                    battleAttack(move);
                    break;
                case MoveData.MoveType.PROTECT:
                    battleProtect(move);
                    break;
                case MoveData.MoveType.ATTACK_UP:
                    battleAttackUp(move);
                    break;
                case MoveData.MoveType.DEFENSE_UP:
                    battleDefenseUp(move);
                    break;
                default:    //unknown move
                    battleShowMoves();
                    break;
            }
            readyToAttack = false;
        }
    }
    public void startBattle(Enemy opponent)
    {
        enemy = opponent;
        
        playerAttackStat = PlayerStats.Instance.attack;
        playerDefenseStat = PlayerStats.Instance.defense;
        playerMaxHealth = PlayerStats.Instance.maxHealth;
        playerCurrentHealth = (int)PlayerStats.Instance.currentHealth;
        enemyAttackStat = enemy.attack;
        enemyDefenseStat = enemy.defense;
        enemyMaxHealth = enemy.maxHealth;
        enemyCurrentHealth = (int)enemy.currentHealth;

        //Opponent.specialEffect
        GameManager.Instance.setState(GameManager.State.Battling);

        buttonText1.text = PlayerStats.Instance.moves[0].moveName;
        buttonText2.text = PlayerStats.Instance.moves[1].moveName;
        buttonText3.text = PlayerStats.Instance.moves[2].moveName;
        buttonText4.text = PlayerStats.Instance.moves[3].moveName;
        playerName.text = PlayerStats.Instance.playerName;
        enemyName.text = enemy.enemyName;


        Debug.Log("Opponent Stats: " + enemyAttackStat + ", " + enemyDefenseStat + ", " + enemy.currentHealth + ", " + enemy.specialEffect);
        Debug.Log("Player Stats: " + playerAttackStat + ", " + playerDefenseStat + ", " + PlayerStats.Instance.currentHealth);
        
        battleShowMenu(); 
    }

    public void battleShowMenu()
    {
        readyToAttack = false;
        playerHP.text = playerCurrentHealth + "/" + playerMaxHealth;
        enemyHP.text = enemyCurrentHealth + "/" + enemyMaxHealth;
        state = battleState.SELECTING;
        DefaultButtons.SetActive(true);
        MoveButtons.SetActive(false);
        battleCamera.enabled = true;
        CameraManager.Instance.moveCameraTo(496, 0);
    }
    public void battleShowMoves()
    {
        state = battleState.ATTACKING;
        DefaultButtons.SetActive(false);
        MoveButtons.SetActive(true);
    }
    public void battleSelectMove1()
    {
        readyToAttack = true;
        GameManager.Instance.setState(GameManager.State.Performing);
        move = PlayerStats.Instance.moves[0];
        Debug.Log("Move selected: " + move.moveName);
        /*switch (move.type)
        {
            case MoveData.MoveType.ATTACK:
                battleAttack(move);
                break;
            case MoveData.MoveType.PROTECT:
                battleProtect(move);
                break;
            case MoveData.MoveType.ATTACK_UP:
                battleAttackUp(move);
                break;
            case MoveData.MoveType.DEFENSE_UP:
                battleDefenseUp(move);
                break;
            default:    //unknown move
                battleShowMoves();
                break;
        }*/
    }

    public void battleSelectMove2()
    {
        readyToAttack = true;
        GameManager.Instance.setState(GameManager.State.Performing);
        move = PlayerStats.Instance.moves[1];
        Debug.Log("Move selected: " + move.moveName);
        /*switch (move.type)
        {
            case MoveData.MoveType.ATTACK:
                battleAttack(move);
                break;
            case MoveData.MoveType.PROTECT:
                battleProtect(move);
                break;
            case MoveData.MoveType.ATTACK_UP:
                battleAttackUp(move);
                break;
            case MoveData.MoveType.DEFENSE_UP:
                battleDefenseUp(move);
                break;
            default:    //unknown move
                battleShowMoves();
                break;
        }*/
    }

    public void battleSelectMove3()
    {
        readyToAttack = true;
        GameManager.Instance.setState(GameManager.State.Performing);
        move = PlayerStats.Instance.moves[2];
        Debug.Log("Move selected: " + move.moveName);
        /*switch (move.type)
        {
            case MoveData.MoveType.ATTACK:
                battleAttack(move);
                break;
            case MoveData.MoveType.PROTECT:
                battleProtect(move);
                break;
            case MoveData.MoveType.ATTACK_UP:
                battleAttackUp(move);
                break;
            case MoveData.MoveType.DEFENSE_UP:
                battleDefenseUp(move);
                break;
            default:    //unknown move
                battleShowMoves();
                break;
        }*/
    }

    public void battleSelectMove4()
    {
        readyToAttack = true;
        GameManager.Instance.setState(GameManager.State.Performing);
        move = PlayerStats.Instance.moves[3];
        Debug.Log("Move selected: " + move.moveName);
        /*switch (move.type)
        {
            case MoveData.MoveType.ATTACK:
                battleAttack(move);
                break;
            case MoveData.MoveType.PROTECT:
                battleProtect(move);
                break;
            case MoveData.MoveType.ATTACK_UP:
                battleAttackUp(move);
                break;
            case MoveData.MoveType.DEFENSE_UP:
                battleDefenseUp(move);
                break;
            default:    //unknown move
                battleShowMoves();
                break;
        }*/

    }

    public void battleAttack(MoveData move)
    {
        //enable the rhythm section
        //use score to calculate attack power
        float playerAttackDamage = playerAttackStat/enemyDefenseStat * move.damage/10f;
        float enemyAttackDamage = enemyAttackStat/playerDefenseStat * 30/10f;
        Debug.Log("player/enemy Attack" + playerAttackDamage + "/" + enemyAttackDamage);
        //deal damage to enemy
        enemy.currentHealth -= playerAttackDamage;
        enemyCurrentHealth = (int)Math.Ceiling(enemy.currentHealth);
        //disable rhythm section
        //enemy damages player
        PlayerStats.Instance.currentHealth -= enemyAttackDamage;
        playerCurrentHealth = (int)Math.Ceiling(PlayerStats.Instance.currentHealth);
        Debug.Log("Player/Enemy health at" + PlayerStats.Instance.currentHealth + ", " + enemy.currentHealth);
        checkIfBattleEnds();
    }

    public void battleProtect(MoveData move)
    {
        Debug.Log("Player protected the attack");
        Debug.Log("Player/Enemy health at" + PlayerStats.Instance.currentHealth + ", " + enemy.currentHealth);
        checkIfBattleEnds();
    }

    public void battleAttackUp(MoveData move)
    {
        Debug.Log("Player attack rose!");
        playerAttackStat = playerAttackStat * 1.5f;

        float enemyAttackDamage = enemyAttackStat/playerDefenseStat * 30/10f;
        PlayerStats.Instance.currentHealth -= enemyAttackDamage;
        playerCurrentHealth = (int)Math.Ceiling(PlayerStats.Instance.currentHealth);
        Debug.Log("Player/Enemy health at" + PlayerStats.Instance.currentHealth + ", " + enemy.currentHealth);

        checkIfBattleEnds();
    }

    public void battleDefenseUp(MoveData move)
    {
        Debug.Log("Player defense rose!");
        playerDefenseStat = playerDefenseStat * 1.5f;

        float enemyAttackDamage = enemyAttackStat/playerDefenseStat * 30/10f;
        PlayerStats.Instance.currentHealth -= enemyAttackDamage;
        playerCurrentHealth = (int)Math.Ceiling(PlayerStats.Instance.currentHealth);
        Debug.Log("Player/Enemy health at" + PlayerStats.Instance.currentHealth + ", " + enemy.currentHealth);

        checkIfBattleEnds();
    }

    public void battleItem(BattleManager.Item item)
    {
        state = battleState.SELECTING;
        //change whatever stat based on the item used

        //enemy damages player

        checkIfBattleEnds();
    }

    public void battleStat()
    {
        state = battleState.SELECTING;
        //displays the player's and enemy's attack and defense stat, their hp, and any modifiers either may have
    }

    public void endBattle(BattleManager.battleResult result)
    {
        //show confirmation message
        if (result == battleResult.WIN)
        {
            Debug.Log("You won!");
        }
        else
        {
            Debug.Log("You lost!");
        }
        //handle rewards
        //close battle menu sprites and background
        battleCamera.enabled = false;
        CameraManager.Instance.moveCameraTo(PlayerStats.Instance.getXPos(), PlayerStats.Instance.getYPos());
        //change game state to roaming
        GameManager.Instance.setState(GameManager.State.Roaming);
    }

    public void checkIfBattleEnds()
    {
        if (enemy.currentHealth <= 0)
        {
            endBattle(battleResult.WIN);
            return;
        }
        if (PlayerStats.Instance.currentHealth <= 0)
        {
            endBattle(battleResult.LOSS);
            return;
        }

        battleShowMenu();
    }
}
