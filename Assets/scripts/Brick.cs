using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour
{
    [Range(1, 5)] public int hitPoints = 1;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateColor();
    }

    public void SetHitPoints(int hp)
    {
        hitPoints = Mathf.Clamp(hp, 1, 5);
        UpdateColor();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        hitPoints--;

        GameManager.Instance.AddScore(1);

        if (hitPoints <= 0)
        {
            Destroy(gameObject);
            StartCoroutine(CheckForLastBrick());
        }
        else
        {
            UpdateColor();
        }
    }

    void UpdateColor()
    {
        switch (hitPoints)
        {
            case 1: sr.color = Color.green; break;
            case 2: sr.color = Color.yellow; break;
            case 3: sr.color = new Color(1f, 0.65f, 0f); break;
            case 4: sr.color = new Color(1f, 0.4f, 0f); break;
            case 5: sr.color = Color.red; break;
        }
    }

    IEnumerator CheckForLastBrick()
    {
        yield return new WaitForEndOfFrame(); // wait until this frame's Destroy completes
        if (FindObjectsOfType<Brick>().Length == 0)
        {
            Debug.Log("[Brick] All bricks destroyed!");
            GameManager.Instance.NextLevel();
        }
    }
}
