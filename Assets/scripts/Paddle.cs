using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 10f;
    public float maxX = 7.5f;

    void Update()
    {
        float move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        Vector3 newPosition = transform.position + new Vector3(move, 0f, 0f);
        newPosition.x = Mathf.Clamp(newPosition.x, -maxX, maxX);
        transform.position = newPosition;
    }
}