using UnityEngine;

public class TileExit : MonoBehaviour
{
    public enum Side {TOP, BOTTOM, LEFT, RIGHT};
    public Side side;
    void OnTriggerEnter2D (Collider2D other)
    {
        //move the camera in that direction (10 up/down, 17.75 l
        //move the player model to the entrance of the following
        switch (side) {
            case Side.TOP:
                Debug.Log("Entered TOP");
                break;
            case Side.BOTTOM:
                Debug.Log("Entered BOTTOM");
                break;
            case Side.LEFT:
                Debug.Log("Entered LEFT");
                break;
            case Side.RIGHT:
                Debug.Log("Entered RIGHT");
                break;
            default:
                return;
        }
    }
}
