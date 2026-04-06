using UnityEngine;

public class NPCBattle : NPC
{
    //public string npcName;
    //public string[] DialogLines;
    //protected bool playerInRange = false;
    //public bool dialogFinished = false;


    public override void Interact()
    {
        //set state to talking
        GameManager.Instance.setState(GameManager.State.Talking);
        //show dialog
        DialogManager.Instance.startDialog(npcName, DialogLines, dialogEnded);
        //start battle/refer to BattleManager
    }

    public void dialogEnded()
    {
        BattleManager.Instance.startBattle(GetComponent<Enemy>());
    }
}