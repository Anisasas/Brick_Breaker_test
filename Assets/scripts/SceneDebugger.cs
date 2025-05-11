using UnityEngine;

public class SceneDebugger : MonoBehaviour
{
    void Start()
    {
        CheckForDuplicates();
    }

    void CheckForDuplicates()
    {
        // Log duplicates of Ball
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        if (balls.Length > 1)
        {
            Debug.LogError("Multiple Balls found in the scene:");
            foreach (GameObject ball in balls)
            {
                Debug.Log("Duplicate Ball: " + ball.name);
            }
        }
        else
        {
            Debug.Log("Ball is unique in the scene.");
        }

        // Log duplicates of GameManager
        GameManager[] managers = GameObject.FindObjectsOfType<GameManager>();
        if (managers.Length > 1)
        {
            Debug.LogError("Multiple GameManagers found in the scene:");
            foreach (GameManager manager in managers)
            {
                Debug.Log("Duplicate GameManager: " + manager.name);
            }
        }
        else
        {
            Debug.Log("GameManager is unique in the scene.");
        }
    }
}
