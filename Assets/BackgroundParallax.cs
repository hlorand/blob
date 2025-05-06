using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    public List<Transform> bgPlanes; // Drag BgPlane1, BgPlane2, ... here (farthest to nearest)
    public List<float> parallaxFactors; // Set values like 0.2f, 0.5f, 0.8f (farthest moves slowest)

    private Vector3 lastCamPos;
    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
        lastCamPos = cam.position;
    }

    void LateUpdate()
    {
        Vector3 delta = cam.position - lastCamPos;
        for (int i = 0; i < bgPlanes.Count; i++)
        {
            // Only move X and Y, keep Z unchanged
            bgPlanes[i].position += new Vector3(delta.x, delta.y, 0) * parallaxFactors[i];
        }
        lastCamPos = cam.position;
    }
}
