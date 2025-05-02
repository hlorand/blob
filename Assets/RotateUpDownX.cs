using UnityEngine;

public class RotateUpDownX : MonoBehaviour
{
    public float rotationSpeed = 90f; // Degrees per second

    void Update()
    {
        float rotate = 0f;

        // W/UpArrow = rotate up (negative X), S/DownArrow = rotate down (positive X)
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            rotate = -1f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            rotate = 1f;

        if (rotate != 0f)
            transform.Rotate(Vector3.right, rotate * rotationSpeed * Time.deltaTime, Space.Self);
    }
}
