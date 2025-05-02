using UnityEngine;

public class CameraScrollZoom : MonoBehaviour
{
    public float zoomSpeed = 5f;

    // Orthographic settings
    public float minOrthoSize = 3f;
    public float maxOrthoSize = 10f;

    // Perspective settings
    public float minFOV = 15f;
    public float maxFOV = 60f;

    private Camera cam;
    private float targetOrthoSize;
    private float targetFOV;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam.orthographic)
            targetOrthoSize = cam.orthographicSize;
        else
            targetFOV = cam.fieldOfView;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            if (cam.orthographic)
            {
                targetOrthoSize -= scroll * zoomSpeed;
                targetOrthoSize = Mathf.Clamp(targetOrthoSize, minOrthoSize, maxOrthoSize);
            }
            else
            {
                targetFOV -= scroll * zoomSpeed * 10f; // Multiply for sensitivity
                targetFOV = Mathf.Clamp(targetFOV, minFOV, maxFOV);
            }
        }

        if (cam.orthographic)
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetOrthoSize, Time.deltaTime * zoomSpeed);
        else
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }
}
