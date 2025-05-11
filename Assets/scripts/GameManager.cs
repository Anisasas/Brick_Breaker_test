using UnityEngine;
using TMPro;  // Include this namespace for TextMeshPro
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int lives = 3;
    public int score;
    public TMP_Text livesText;  // Change to TMP_Text for TextMeshPro
    public TMP_Text scoreText;  // Change to TMP_Text for TextMeshPro
    public Ball ball;
    public Transform paddle;

    void Start()
    {
        UpdateUI();
    }

    public void LoseLife()
    {
        lives--;
        UpdateUI();
        if (lives <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            ball.ResetBall();
            paddle.position = new Vector3(0f, -4f, 0f);
        }
    }

    public void AddScore(int pts)
    {
        score += pts;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (livesText != null && scoreText != null)  // Safe check
        {
            livesText.text = "Lives: " + lives;
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogError("UI Text references are not assigned! Please assign LivesText and ScoreText in the Inspector.");
        }
    }
}
