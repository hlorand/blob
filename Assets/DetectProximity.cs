using System.Collections.Generic;
using UnityEngine;

public class DetectProximity : MonoBehaviour
{
    [Header("Raycast Settings")]
    [Tooltip("The length of the rays used for detection.")]
    public float rayLength = 5f; // Set the length of the rays

    public List<GameObject> detectedObjects = new List<GameObject>(); // List to store detected objects

    public bool attached = false; // Flag to check if the object is attached

    public List<GameObject> attachedObjects = new List<GameObject>(); // List to store attached objects

    private Vector3[] directions = new Vector3[]
    {
        Vector3.up, // North
        (Vector3.up + Vector3.right).normalized, // Northeast
        Vector3.right, // East
        (Vector3.right + Vector3.down).normalized, // Southeast
        Vector3.down, // South
        (Vector3.up + Vector3.left).normalized, // Northwest
        Vector3.left, // West
        (Vector3.left + Vector3.down).normalized // Southwest
    };

    void Update()
    {
        DetectObjects();

        // Attach object with a spring joint to this object if it is not already attached and has the tag "blob"
        if (!attached)
        {
            foreach (var detectedObject in detectedObjects)
            {
                if (detectedObject.CompareTag("blob"))
                {
                    SpringJoint springJoint = gameObject.AddComponent<SpringJoint>();
                    springJoint.connectedBody = detectedObject.GetComponent<Rigidbody>();
                    springJoint.spring = 10f; // Adjust the spring strength as needed
                    springJoint.damper = 1f; // Adjust the damper value as needed
                    attached = true; // Set the attached flag to true
                    // Add the detected object to the attached objects list
                    attachedObjects.Add(detectedObject);

                    // Add a LineRenderer to the detected object if not already present
                    if (detectedObject.GetComponent<LineRenderer>() == null)
                    {
                        LineRenderer lineRenderer = detectedObject.AddComponent<LineRenderer>();
                        lineRenderer.startWidth = 0.1f;
                        lineRenderer.endWidth = 0.1f;
                        lineRenderer.positionCount = 2;
                        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                        lineRenderer.startColor = Color.red;
                        lineRenderer.endColor = Color.red;
                    }
                    
                    
                }
            }
        }

        // Update the position of the LineRenderer for every attached object
        foreach (var attachedObject in attachedObjects)
        {
            LineRenderer lineRenderer = attachedObject.GetComponent<LineRenderer>();
            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, attachedObject.transform.position);
            }
        }
    }

    private void DetectObjects()
    {
        detectedObjects.Clear(); // Clear the list before detecting objects

        foreach (var direction in directions)
        {
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;

            Debug.DrawRay(transform.position, direction * rayLength, Color.red); // Visualize the ray in the scene view


            if (Physics.Raycast(ray, out hit, rayLength))
            {

                if (!detectedObjects.Contains(hit.collider.gameObject))
                {
                    detectedObjects.Add(hit.collider.gameObject); // Add the detected object to the list
                }
            }
        }
    }
}
