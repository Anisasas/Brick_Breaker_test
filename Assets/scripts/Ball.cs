using UnityEngine;

public class Ball : MonoBehaviour
{
    public float startSpeed = 6f;
    private Rigidbody2D rb;
    private Transform paddle;
    private bool launched;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        paddle = GameObject.FindGameObjectWithTag("Paddle").transform;
        ResetBall();
    }

    void Update()
    {
        if (!launched)
        {
            transform.position = paddle.position + Vector3.up * 0.5f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = Vector2.up * startSpeed;
                launched = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Paddle"))
        {
            float hitFactor = (transform.position.x - paddle.position.x) / (paddle.localScale.x / 2f);
            Vector2 dir = new Vector2(hitFactor, 1).normalized;
            rb.velocity = dir * startSpeed;
        }
        else if (col.gameObject.CompareTag("Bottom"))
        {
            GameManager.Instance.LoseLife();
            ResetBall();
        }
    }

    public void ResetBall()
    {
        launched = false;
        rb.velocity = Vector2.zero;
        transform.position = paddle.position + Vector3.up * 0.5f;
    }
}
