using UnityEngine;
using TMPro;
using System;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;
    public Canvas DialogCanvas;
    public TMP_Text TMP_npcName; 
    public TMP_Text TMP_Dialog;
    private string[] currentLines;
    private string npcName;
    private int dialogIndex;
    private System.Action onDialogueComplete;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        DialogCanvas = GetComponent<Canvas>();
        DialogCanvas.enabled = false;
    }

    void Update ()
    {
        if (GameManager.Instance.getState() == GameManager.State.Talking)
        {
            if(Input.GetKeyDown(KeyCode.Return)) nextLine();
        }
    }

    public void startDialog(string FuncnpcName, string[] dialog, System.Action onComplete)
    {
        onDialogueComplete = onComplete;
        currentLines = dialog;
        npcName = FuncnpcName;
        dialogIndex = 0;
        DialogCanvas.enabled = true;
        TMP_npcName.text = npcName;
        Debug.Log(npcName + " says: " + dialog[0]);
        TMP_Dialog.text = dialog[0];
    }

    public void nextLine()
    {

        dialogIndex++;
        if (dialogIndex >= currentLines.Length)
        {
            endDialogue();
            return;
        }
        Debug.Log(npcName + " says: " + currentLines[dialogIndex]);
        TMP_Dialog.text = currentLines[dialogIndex];
    }

    public void endDialogue()
    {
        DialogCanvas.enabled = false;
        GameManager.Instance.setState(GameManager.State.Roaming);
        dialogIndex = 0;
        currentLines = null;

        if (onDialogueComplete != null)
        {
            onDialogueComplete.Invoke();
            onDialogueComplete = null;
        }
    }
}
