using UnityEngine;

public class Ball : MonoBehaviour
{
    public float startSpeed = 20f;
    private Rigidbody2D rb;
    private Transform paddle;
    private bool launched;
    private bool isFalling;

    // Debug guards
    private bool debugLaunched = false;
    private bool debugHitBottom = false;
    private bool debugReset = false;

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
                launched = true;
                isFalling = false;

                if (!debugLaunched)
                {
                    Debug.Log("Ball Launched!");
                    debugLaunched = true;
                }

                rb.velocity = Vector2.up * startSpeed;

                // Reset bottom hit debug after relaunch
                debugHitBottom = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bottom") && launched && !isFalling)
        {
            isFalling = true;
            launched = false;

            if (!debugHitBottom)
            {
                Debug.Log("Ball Hit Bottom");
                debugHitBottom = true;
            }

            GameManager.Instance.LoseLife();
            ResetBall();
        }
        else if (col.gameObject.CompareTag("Paddle"))
        {
            float hit = (transform.position.x - paddle.position.x) / (paddle.localScale.x / 2f);
            Vector2 dir = new Vector2(hit, 1).normalized;
            rb.velocity = dir * startSpeed;
        }
    }

    public void ResetBall()
    {
        rb.velocity = Vector2.zero;
        transform.position = paddle.position + Vector3.up * 0.5f;
        launched = false;

        if (!debugReset)
        {
            Debug.Log("Ball Reset to Paddle Position");
            debugReset = true;
        }

        // Reset launch debug when ball is reset
        debugLaunched = false;
    }
}
