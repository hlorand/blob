using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public float maxY;
    public float minY;
    public float maxX;
    public float minX;
    public float smoothSpeed = 0.1f; // How fast the camera moves back

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null || !cam.orthographic)
        {
            Debug.LogError("Attach this script to an orthographic camera.");
            enabled = false;
        }
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;

        // Clamp camera position to bounds
        float clampedX = Mathf.Clamp(pos.x, minX, maxX);
        float clampedY = Mathf.Clamp(pos.y, minY, maxY);

        Vector3 targetPos = new Vector3(clampedX, clampedY, pos.z);

        // Smoothly move camera back inside bounds
        transform.position = Vector3.Lerp(pos, targetPos, smoothSpeed);
    }
}
