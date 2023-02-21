using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public int ID;
    public string Type;
    public string Title;
    public string Status;
    public string Description;
    public Sprite Icon;
    public int Rarity;

    public Dictionary<string, float> Stats = new Dictionary<string, float>();
    
    // C-Tor to create an Item
    public InventoryItem(string type, int rarity)
    {
        Type = type;
        Rarity = rarity;
    }

    // Use Item to Create Item
    public InventoryItem(InventoryItem item)
    {
        ID = item.ID;
        Type = item.Type;
        Title = item.Title;
        Status = item.Status;
        Description = item.Description;
        Icon = Resources.Load<Sprite>("Items/" +item.Title +item.Type);
        Rarity = item.Rarity;
        Stats = item.Stats;
    }
}