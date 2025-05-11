using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hits = 1; // Number of hits before the brick is destroyed
    public int points = 1; // 1 point per brick hit
    public GameManager gm;

    void Start()
    {
        if (gm == null)
        {
            gm = FindObjectOfType<GameManager>();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        hits--;
        if (hits <= 0)
        {
            gm.AddScore(points);
            Destroy(gameObject);
        }
    }
}