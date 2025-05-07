using UnityEngine;

/*
Meshcollider hozzáadása minden gyerekhez
----------------------------------------
Így nem nekem kell kézzel hozzáadni minden egyes gyerekhez a meshcollidert, hanem a script megteszi helyettem.
A legtöbb 3d modell ezt használja. Önmagunkhoz is adunk ha nincs
*/
public class AddMeshcollider : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // önmagunkhoz is adunk ha nincs
        if (GetComponent<MeshCollider>() == null)
        {
            // meshcollider hozzáadása
            gameObject.AddComponent<MeshCollider>();

            // set convex to true
            GetComponent<MeshCollider>().convex = true;
        }

        //gyerekek lekérése
        Transform[] children = GetComponentsInChildren<Transform>();
        // meshcollider hozzáadása minden gyerekhez
        foreach (Transform child in children)
        {
            // ha nincs még meshcollider
            if (child.GetComponent<MeshCollider>() == null)
            {
                // meshcollider hozzáadása
                child.gameObject.AddComponent<MeshCollider>();

                // set convex to true
                child.GetComponent<MeshCollider>().convex = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}