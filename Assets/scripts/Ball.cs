using UnityEngine;

public class Ball : MonoBehaviour
{
    public float startSpeed = 5f;
    public Rigidbody2D rb;
    public Transform paddle;
    public bool launched;
    public GameManager gm;

    void Start()
    {
        ResetBall();
    }

    void Update()
    {
        if (!launched)
        {
            transform.position = paddle.position + Vector3.up * 0.6f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(Random.Range(-1f, 1f), 1f).normalized * startSpeed;
                launched = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bottom"))
        {
            gm.LoseLife();
        }

        if (col.gameObject.CompareTag("Paddle"))
        {
            float hitX = transform.position.x - col.transform.position.x;
            float maxOffset = col.collider.bounds.size.x / 2;
            float normalized = hitX / maxOffset;

            Vector2 newDirection = new Vector2(normalized, 1f).normalized;
            rb.velocity = newDirection * startSpeed;
        }
    }

    public void ResetBall()
    {
        rb.velocity = Vector2.zero;
        launched = false;
    }
}