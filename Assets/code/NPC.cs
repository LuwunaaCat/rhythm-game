using UnityEngine;

public class NPC : MonoBehaviour
{
    public string npcName;
    public string[] DialogLines;
    protected bool playerInRange = false;
    public bool dialogFinished = false;
    protected bool hasInteracted = false;

    protected void Update()
    {
        if (playerInRange == true && GameManager.Instance.getState() == GameManager.State.Roaming && 
            Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRange = true;
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRange = false;
    }

    public virtual void Interact()
    {
        GameManager.Instance.setState(GameManager.State.Talking);
        DialogManager.Instance.startDialog(npcName, DialogLines, null);
    }
}