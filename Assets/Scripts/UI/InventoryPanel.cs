using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryPanel : MonoBehaviour
{
    [Header("Assign 5 slot parents (top to bottom)")]
    public List<Transform> slotParents; // Drag Slot0..Slot4 here in order

    [Header("Fill with prefabs in Inspector")]
    public List<GameObject> inventoryPrefabs;

    void Start()
    {
        UpdateInventoryUI();
    }

    void UpdateInventoryUI()
    {
        int count = inventoryPrefabs.Count;
        for (int i = 0; i < slotParents.Count; i++)
        {
            // Clear previous children
            foreach (Transform child in slotParents[i])
                Destroy(child.gameObject);

            // Get the corresponding prefab (last 5)
            int prefabIndex = count - slotParents.Count + i;
            if (prefabIndex >= 0 && prefabIndex < count)
            {
                GameObject go = Instantiate(inventoryPrefabs[prefabIndex], slotParents[i]);
                // Optionally, adjust the instantiated object (scale, disable scripts, etc.)
            }
        }
    }
}
