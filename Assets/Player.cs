using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<GameObject> cannonballPrefabs; // Assign in Inspector
    public float launchForce = 20f;            // Speed of cannonball

    private int currentIndex = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1))
        {
            FireCannonball();
        }
        
        if (Input.GetKeyDown(KeyCode.A) )
        {
            transform.localScale = Vector3.Scale(transform.localScale, new Vector3(1f, 1f, 1.05f));
            launchForce += 5f;
            if (launchForce > 100f)
                launchForce = 100f;
        }
        if (Input.GetKeyDown(KeyCode.D) )
        {
            if (launchForce <= 15f){
                launchForce = 15f;
            } else{
                transform.localScale = Vector3.Scale(transform.localScale, new Vector3(1f, 1f, 0.95f));
                launchForce -= 5f;
            }
        }
    }


    void FireCannonball()
    {
        if (cannonballPrefabs == null || cannonballPrefabs.Count == 0)
            return;

        GameObject prefab = cannonballPrefabs[currentIndex];
        GameObject cannonball = Instantiate(prefab, transform.position, Quaternion.identity);

        // set cannonball's active variable to true (its a DetectProximityAndAttach script)
        DetectProximityAndAttach detectProximityAndAttach = cannonball.GetComponent<DetectProximityAndAttach>();
        if (detectProximityAndAttach != null)
        {
            detectProximityAndAttach.active = true;
        }

        // Use the pipe's forward direction (blue arrow in Scene view)
        Vector3 launchDir = transform.forward;
        launchDir.Normalize();

        Rigidbody rb = cannonball.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = launchDir * launchForce;

        currentIndex = (currentIndex + 1) % cannonballPrefabs.Count;
    }


}
