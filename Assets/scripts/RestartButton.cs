using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    public Button restartButton;

    void Awake()
    {
        if (restartButton == null)
            restartButton = GetComponent<Button>();
    }

    void Start()
    {
        if (restartButton != null)
            restartButton.onClick.AddListener(() => GameManager.Instance.RestartGame());
        else
            Debug.LogError("RestartButton: no Button component found or assigned.");
    }
}
