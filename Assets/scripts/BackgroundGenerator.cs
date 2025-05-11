using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    public GameObject brickPrefab;
    public int rows = 5;
    public int columns = 10;
    public float spacing = 0.1f;
    public Transform brickParent;
    public float brickWidth = 1f;
    public float brickHeight = 0.5f;
    public float topOffset = 2f;

    void Awake()
    {
        if (brickParent == null)
        {
            var go = new GameObject("BrickParent");
            brickParent = go.transform;
        }
    }

    public void GenerateBricks()
    {
        foreach (Transform child in brickParent) Destroy(child.gameObject);

        float startX = -columns / 2f * (brickWidth + spacing) + (brickWidth / 2f);
        float startY = Camera.main.orthographicSize - topOffset;

        for (int row = 0; row < rows; row++)
            for (int col = 0; col < columns; col++)
                if (Random.value < 0.7f)
                {
                    var pos = new Vector3(
                        startX + col * (brickWidth + spacing),
                        startY - row * (brickHeight + spacing),
                        0);
                    var brick = Instantiate(brickPrefab, pos, Quaternion.identity, brickParent);
                    var bs = brick.GetComponent<Brick>();
                    if (bs != null) bs.SetHitPoints(Random.Range(1, 6));
                }
    }
}
