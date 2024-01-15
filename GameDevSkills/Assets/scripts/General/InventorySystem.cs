using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public List<GameObject> inventoryItems = new List<GameObject>();

    // Add an item to the inventory
    public void AddItem(GameObject item)
    {
        inventoryItems.Add(item);
        // You can add additional logic here, such as updating UI or performing other actions
    }

    // Check if the inventory contains a specific item
    public bool HasItem(GameObject item)
    {
        return inventoryItems.Contains(item);
    }

    // Remove an item from the inventory
    public void RemoveItem(GameObject item)
    {
        inventoryItems.Remove(item);
        // You can add additional logic here, such as updating UI or performing other actions
    }
}
