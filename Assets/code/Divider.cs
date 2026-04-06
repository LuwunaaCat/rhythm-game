using UnityEngine;

public class Divider : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        //if the note becomes too low (below the screen), destroy it
        if (transform.position.y <= -5f)
        {
            Destroy(gameObject);
        }
//        Debug.Log("Note position: " + transform.position);
    }

    public void DestroyNote()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}