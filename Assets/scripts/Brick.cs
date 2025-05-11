using UnityEngine;

public class Brick : MonoBehaviour
{
    private int hitPoints;

    public void SetHitPoints(int points)
    {
        hitPoints = points;
    }

    public int GetHitPoints()
    {
        return hitPoints;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Handle collision logic
        hitPoints--;
        if (hitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
