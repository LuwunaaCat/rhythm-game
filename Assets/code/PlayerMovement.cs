using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public static PlayerMovement Instance;

    void Awake ()
    {
        Instance = this;
    }

    void Update()
    {
        if (GameManager.Instance.canMove) {
            float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows
            float moveY = Input.GetAxis("Vertical");   // W/S or Up/Down arrows
            transform.position += new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime;
            CameraManager.Instance.moveCameraBy(moveX * moveSpeed * Time.deltaTime, moveY * moveSpeed * Time.deltaTime);
        }        
    }
}
