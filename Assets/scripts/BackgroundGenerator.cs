using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    public GameObject brickPrefab;
    public int rows = 10;
    public int columns = 20;
    public float spacing = 0f;
    public Vector2 startPos = new Vector2(-9f, 4.5f);

    void Start()
    {
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                Vector2 pos = startPos + new Vector2(x * (1 + spacing), -y * (0.5f + spacing));
                GameObject b = Instantiate(brickPrefab, pos, Quaternion.identity, transform);

                // Randomly set the hit points and color
                int hitPoints = Random.Range(1, 6); // Random hit points (1-5)
                b.GetComponent<Brick>().hits = hitPoints;
                SetBrickColor(b, hitPoints);

                b.GetComponent<SpriteRenderer>().sortingOrder = -10; // Send it behind gameplay
            }
        }
    }

    // Method to set the color based on hit points
    void SetBrickColor(GameObject brick, int hitPoints)
    {
        SpriteRenderer sr = brick.GetComponent<SpriteRenderer>();

        // Set color based on hit points
        switch (hitPoints)
        {
            case 1:
                sr.color = new Color(0.96f, 0.20f, 0.20f); // F73432 color
                break;
            case 2:
                sr.color = new Color(0.80f, 0.80f, 0.20f); // Yellowish
                break;
            case 3:
                sr.color = new Color(0.20f, 0.96f, 0.20f); // Green
                break;
            case 4:
                sr.color = new Color(0.20f, 0.20f, 0.96f); // Blue
                break;
            case 5:
                sr.color = Color.red; // Red for max hits
                break;
        }
    }
}
