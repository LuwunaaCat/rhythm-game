using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State {Roaming, Talking, Battling, inMenu, Performing};
    public State state;
    public bool canMove;
    public static GameManager Instance;
    void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setState(State.Roaming);
    }

    public void setState(State funcState)
    {
        switch (funcState)
        {
            case State.Roaming:
                state = State.Roaming;
                canMove = true;
                //set camera to player
                break;
            case State.Talking:
                state = State.Talking;
                canMove = false;
                break;
            case State.Battling:
                state = State.Battling;
                canMove = false;
                break;
                //set camera to battle stage
            case State.Performing:
                state = State.Performing;
                canMove = false;
                break;
            default:    //in menu
                state = State.inMenu;
                canMove = false;
                //enable menu canvas
                break;
        }
    }

    public State getState()
    {
        switch (state)
        {
            case State.Roaming:
                return State.Roaming;
            case State.Talking:
                return State.Talking;
            case State.Battling:
                return State.Battling;
            case State.Performing:
                return State.Performing;
            default:
                return State.inMenu;
        }
    }
}
