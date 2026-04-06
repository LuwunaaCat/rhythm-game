using UnityEngine;

public class Note : MonoBehaviour
{
    public KeyCode laneKey;    //D, F, J, or K depending on the lane
    private Collider2D noteCollider;
    public bool isHit = false;
    void Start()
    {
        noteCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        //if the note becomes too low (below the screen), destroy it
        if (transform.position.y <= -7f)
        {
            DestroyNote();
        }
    }

    public void DestroyNote()
    {
        noteCollider.enabled = false;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}