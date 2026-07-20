using System.Collections.Generic;
using UnityEngine;

// Gắn 1 lần vào GameManager (cùng chỗ có PuzzleCodeCollector_TuanAnh)
public class PlayerInventory_TuanAnh : MonoBehaviour
{
    private HashSet<string> items = new HashSet<string>();

    public void AddItem(string itemName)
    {
        items.Add(itemName);
        Debug.Log($"Đã nhặt: {itemName}");
    }

    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }

    public void RemoveItem(string itemName)
    {
        items.Remove(itemName);
    }
}
