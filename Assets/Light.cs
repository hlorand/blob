using UnityEngine;

public class Light : MonoBehaviour
{
    public bool turnedon = false; // Light is turned on or off

    public Material bulbok; // Material to be used for the light
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if turned on search for material "bulbok" and set this to children named "Cube"
        if (turnedon)
        {
            Debug.Log("Light is turned on");
            // search for material "bulbok" and set this to children named "Cube"
            if (bulbok != null)
            {
                Debug.Log("bulbok material found");
                Transform cube = transform.Find("Cube");
                if (cube != null)
                {
                    Debug.Log("Cube found");
                    Renderer renderer = cube.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        Debug.Log("Renderer found");
                        renderer.material = bulbok;
                    }
                }
            }
        }
        
    }
}
