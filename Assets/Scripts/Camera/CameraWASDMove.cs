using UnityEngine;

public class CameraWASDMove : MonoBehaviour
{
    public float moveSpeed = 30f; // Movement speed in units/second

    void Update()
    {
        Vector3 move = Vector3.zero;

        // WASD or Arrow keys
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            move.x = -1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            move.x = 1;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            move.y = 1;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            move.y = -1;

        // Normalize for diagonal movement and apply speed
        if (move != Vector3.zero)
            transform.position += move.normalized * moveSpeed * Time.deltaTime;
    }
}
