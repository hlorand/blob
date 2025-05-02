using UnityEngine;

public class EdgeScrollCamera : MonoBehaviour
{
    public float edgeSize = 100f;      // Edge detection size in pixels
    public float moveSpeed = 30f;      // Camera move speed in units/second

    void Update()
    {
        Vector3 move = Vector3.zero;

        // Horizontal movement
        if (Input.mousePosition.x < edgeSize)
            move.x = -1;
        else if (Input.mousePosition.x > Screen.width - edgeSize)
            move.x = 1;

        // Vertical movement
        if (Input.mousePosition.y < edgeSize)
            move.y = -1;
        else if (Input.mousePosition.y > Screen.height - edgeSize)
            move.y = 1;

        // Normalize for diagonal movement and apply speed
        if (move != Vector3.zero)
            transform.position += move.normalized * moveSpeed * Time.deltaTime;
    }
}
