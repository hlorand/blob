using UnityEngine;
using System.Collections.Generic;

public class DetectProximityAndAttach : MonoBehaviour
{
    public float detectionRadius = 5f;
    public int maxAttachedObjects = 5;

    public List<GameObject> attachedObjects = new List<GameObject>();
    private List<SpringJoint> springJoints = new List<SpringJoint>();
    private List<LineRenderer> lineRenderers = new List<LineRenderer>();

    public bool active = false;

    void Start()
    {
        attachedObjects = new List<GameObject>();
        springJoints = new List<SpringJoint>();
        lineRenderers = new List<LineRenderer>();
    }

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        List<GameObject> detectedObjects = new List<GameObject>();

        foreach (var hitCollider in hitColliders)
        {
            GameObject obj = hitCollider.gameObject;

            if (obj != gameObject &&
                obj.CompareTag("blob") &&
                !attachedObjects.Contains(obj) &&
                obj.GetComponent<Rigidbody>() != null)
            {
                detectedObjects.Add(obj);
            }
        }

        foreach (var detectedObject in detectedObjects)
        {
            if (attachedObjects.Count < maxAttachedObjects)
            {
                AttachGameObject(detectedObject);
            }
        }

        UpdateLineRenderers();
    }

    void AttachGameObject(GameObject detectedObject)
    {
        if( !active ) return;

        // defore attaching detect if the object is already attached (see its attachedObjects list) skip if it is already attached
        if (attachedObjects.Contains(detectedObject))
        {
            return;
        }

        // Spring Joint
        SpringJoint joint = gameObject.AddComponent<SpringJoint>();
        joint.connectedBody = detectedObject.GetComponent<Rigidbody>();
        joint.spring = 1000f;
        joint.damper = 100f;
        joint.maxDistance = 0.5f;
        joint.enableCollision = false;
        joint.tolerance = 0.2f;
        joint.axis = new Vector3(0, 0, 0); // Set the axis of the spring joint to Z-axis
        joint.anchor = Vector3.zero; // Set the anchor point to the center of the object

        // Add to lists
        attachedObjects.Add(detectedObject);
        springJoints.Add(joint);

        // Line Renderer
        GameObject lineObj = new GameObject("LineTo_" + detectedObject.name);
        lineObj.transform.parent = this.transform;

        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.startColor = Color.red;
        lr.endColor = Color.red;
        lr.useWorldSpace = true;
        lr.positionCount = 2;
        lr.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        lineRenderers.Add(lr);
    }

    void UpdateLineRenderers()
    {
        for (int i = 0; i < lineRenderers.Count; i++)
        {
            if (attachedObjects[i] != null)
            {
                lineRenderers[i].SetPosition(0, transform.position); // Origin
                lineRenderers[i].SetPosition(1, attachedObjects[i].transform.position); // Target
                lineRenderers[i].enabled = true;
            }
            else
            {
                lineRenderers[i].enabled = false;
            }
        }
    }

    public void RemoveAttachedGameObject(GameObject detectedObject)
    {
        int index = attachedObjects.IndexOf(detectedObject);
        if (index != -1)
        {
            Destroy(springJoints[index]);
            Destroy(lineRenderers[index].gameObject);

            springJoints.RemoveAt(index);
            lineRenderers.RemoveAt(index);
            attachedObjects.RemoveAt(index);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    // if its touching the "ground" tag freeze position, if its not touching the ground unfreeze position
    void OnCollisionEnter(Collision collision)
    {
        if( !active ) return;

        if (collision.gameObject.CompareTag("ground"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY; // allow Z rotation when on ground
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if( !active ) return;

        if (collision.gameObject.CompareTag("ground"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                // unfreeze position and freeze rotation
                rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            }
        }
    }
}
