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

    public int startingLives = 3; // Editable in Unity
    private int score, lives, level;
    private bool lifeLostInProgress = false;
    private bool scoreAddedThisFrame = false;
    private bool levelCompleteTriggered = false; // New flag to prevent double triggers

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (bgGen == null) bgGen = FindObjectOfType<BackgroundGenerator>();
        if (ball == null) ball = FindObjectOfType<Ball>();

        score = 0;
        lives = startingLives;
        level = 1;
        Time.timeScale = 1;
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        UpdateUI();
        bgGen.GenerateBricks();
    }

    void Update()
    {
        lifeLostInProgress = false;
        scoreAddedThisFrame = false;

        // Check for level complete
        if (!levelCompleteTriggered && FindObjectsOfType<Brick>().Length == 0)
        {
            levelCompleteTriggered = true;
            Invoke(nameof(NextLevel), 1f); // Delay for effect
        }
    }

    void UpdateUI()
    {
        scoreText.text = $"Score: {score}";
        livesText.text = $"Lives: {lives}";
        levelText.text = $"Level: {level}";
    }

    public void AddScore(int pts)
    {
        if (scoreAddedThisFrame) return;

        scoreAddedThisFrame = true;
        score += pts;
        UpdateUI();
    }

    public void LoseLife()
    {
        if (lifeLostInProgress) return;
        lifeLostInProgress = true;

        if (lives <= 0) return;

        lives--;
        if (lives < 0) lives = 0;
        UpdateUI();

        if (lives == 0)
        {
            if (gameOverPanel != null) gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            ball.ResetBall();
        }
    }

    public void NextLevel()
    {
        level++;
        levelCompleteTriggered = false;
        UpdateUI();
        bgGen.GenerateBricks();
        ball.ResetBall();
    }

    public void RestartGame()
    {
        score = 0;
        lives = startingLives;
        level = 1;
        levelCompleteTriggered = false;
        Time.timeScale = 1;
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        UpdateUI();
        bgGen.GenerateBricks();
        ball.ResetBall();
    }

    public int GetLevel()
    {
        return level;
    }
}
