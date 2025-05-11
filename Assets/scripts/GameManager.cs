using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public TMP_Text scoreText;
    public TMP_Text livesText;
    public TMP_Text levelText;
    public GameObject gameOverPanel;

    public BackgroundGenerator bgGen;
    public Ball ball;

    int score, lives, level;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (bgGen == null) 
            bgGen = FindObjectOfType<BackgroundGenerator>();
        if (ball == null)  
            ball  = FindObjectOfType<Ball>();

        if (gameOverPanel == null)
        {
            gameOverPanel = GameObject.Find("GameOverPanel");
            if (gameOverPanel == null)
                Debug.LogError("GameManager: No GameOverPanel assigned or found in scene. Rename your panel to 'GameOverPanel' or assign it in the Inspector.");
        }

        score = 0;
        lives = 3;
        level = 1;
        Time.timeScale = 1;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        UpdateUI();
        bgGen.GenerateBricks();
    }


    void UpdateUI()
    {
        scoreText.text = $"Score: {score}";
        livesText.text  = $"Lives: {lives}";
        levelText.text  = $"Level: {level}";
    }

    public void AddScore(int pts)
    {
        score += pts;
        UpdateUI();
    }

    public void LoseLife()
    {
        if (lives <= 0) return;        // already game over

        lives--;
        if (lives < 0) lives = 0;      // clamp

        UpdateUI();

        if (lives == 0)
        {
            GameOver();
        }
        else
        {
            ball.ResetBall();
            bgGen.GenerateBricks();
        }
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        score = 0;
        lives = 3;
        level = 1;
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);

        UpdateUI();
        ball.ResetBall();
        bgGen.GenerateBricks();
    }
}
