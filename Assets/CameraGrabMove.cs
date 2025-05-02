using UnityEngine;

public class CameraGrabMove : MonoBehaviour
{
    public float dragSpeed = 1f; // Adjust for sensitivity

    private Vector3 dragOrigin;
    private bool isDragging = false;

    void Update()
    {
        // Start dragging
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }

        // Stop dragging
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // Move camera while dragging
        if (isDragging)
        {
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 difference = dragOrigin - currentPos;
            transform.position += difference * dragSpeed;
        }
    }
}
