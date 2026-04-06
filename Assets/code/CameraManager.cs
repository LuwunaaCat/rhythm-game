using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        transform.position = new Vector3(0, 0, -10);
    }

    //used to match the camera to player movement
    public void moveCameraBy(float x, float y)
    {
        if (GameManager.Instance.canMove) {
            transform.position += new Vector3(x, y, 0);
        }
    }

    //used to move the camera to a specific place for whatever reason
    public void moveCameraTo(float x, float y)
    {
        transform.position = new Vector3(x, y, -10);
    }
}
