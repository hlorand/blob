using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<GameObject> cannonballPrefabs; // Assign in Inspector
    public float launchForce = 20f;            // Speed of cannonball

    private int currentIndex = 0;

    public GameObject nextButton; // Assign in Inspector

    private AudioSource audioSource;

    void Start()
    {
        // innét kell a cannonballPrefabs listába a cannonball prefabokat
        GameObject.Find("Blob Prefabs").SetActive(false);
        //GameObject.Find("HelpImage").SetActive(true);

        // serach for gameobject named CannonballMagazine and place the cannonballPrefabs in it
        // with position starting from the CAnnonballMagazine's position and set a little gap between them in the x direction
        GameObject cannonballMagazine = GameObject.Find("CannonballMagazine");
        if (cannonballMagazine != null)
        {
            Vector3 startPos = cannonballMagazine.transform.position;
            for (int i = 0; i < cannonballPrefabs.Count; i++)
            {
                // instantiate the prefab and witch the prefab with the instance
                GameObject cannonball = Instantiate(cannonballPrefabs[i], startPos - new Vector3(i * 2f, 0, 0), Quaternion.identity);
                cannonballPrefabs[i] = cannonball;

                //RepositionCannonball(cannonballPrefabs[i].GetComponent<Rigidbody>(), startPos - new Vector3(i * 2f, 0, 0));
                cannonballPrefabs[i].transform.parent = cannonballMagazine.transform;
                //cannonball.SetActive(false); // Deactivate the cannonball
            }
        }
        else
        {
            Debug.LogError("CannonballMagazine not found!");
        }

        // kezdeti ágyúcső hossz
        transform.localScale = new Vector3( transform.localScale.x, transform.localScale.y, launchForce/20 );


        // audiosource komponens lekérése
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1))
        {
            audioSource.Play();
            FireCannonball();
        }
        
        if (Input.GetKeyDown(KeyCode.E) )
        {
            launchForce += 5f;

            if (launchForce > 150f)
                launchForce = 150f;
            
            transform.localScale = new Vector3( transform.localScale.x, transform.localScale.y, launchForce/20 ); //Vector3.Scale(transform.localScale, new Vector3(1f, 1f, 1.05f));
            
        }
        if (Input.GetKeyDown(KeyCode.Q) )
        {
            launchForce -= 5f;
            
            if (launchForce <= 15f){
                launchForce = 15f;
            } 

            transform.localScale = new Vector3( transform.localScale.x, transform.localScale.y, launchForce/20 );
        }

        // find all gameobject that has tag "light" and check if every one has "Light class" > turnedon = true, if so find NExButtonBIG and set active
        GameObject[] lights = GameObject.FindGameObjectsWithTag("light");
        bool allLightsOn = true;
        foreach (GameObject light in lights)
        {
            Light lightComponent = light.GetComponent<Light>();
            if (lightComponent != null && !lightComponent.turnedon)
            {
                allLightsOn = false;
                break;
            }
        }
        if (nextButton != null)
        {
            nextButton.SetActive(allLightsOn);
        }

    }

    void RepositionCannonball(Rigidbody rb, Vector3 newPosition)
        {
            // setactive false before repos
            rb.gameObject.SetActive(false);
            if (rb != null && rb.constraints.HasFlag(RigidbodyConstraints.FreezePositionZ))
            {
            // Temporarily disable Z position constraint
            rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
            rb.transform.position = newPosition;
            // Re-enable Z position constraint
            rb.constraints |= RigidbodyConstraints.FreezePositionZ;
            }
            else
            {
            rb.transform.position = newPosition;
            }
            rb.gameObject.SetActive(true);

        }

    void FireCannonball()
    {
        if (cannonballPrefabs == null || cannonballPrefabs.Count == 0)
            return;

        GameObject cannonball = cannonballPrefabs[0];
        cannonballPrefabs.RemoveAt(0);


        RepositionCannonball(cannonball.GetComponent<Rigidbody>(), transform.position);

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
    }


}
