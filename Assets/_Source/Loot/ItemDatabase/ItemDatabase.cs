using System;
using System.Collections.Generic;
using DataBase;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemDatabase : MonoBehaviour
{
    [SerializeField] private List<String> titles = new List<string>() {"Common", "Uncommon", "Rare", "Legendary"};
    [SerializeField] private List<String> types = new List<string>() {"Sword", "Armor"};
    [SerializeField] [Range(0,4)] int lootRarity;

    public int LootRarity
    {
        get => lootRarity;
        set => lootRarity = value;
    }

    public List<InventoryItem> DataBaseItems;
    private int itemsToCreate = 1_270_137;

    public int ItemsToCreate => itemsToCreate;
    
    [Header("Item Settings")]
    [SerializeField] private ItemSettings common;
    [SerializeField] private ItemSettings uncommon;
    [SerializeField] private ItemSettings rare;
    [SerializeField] private ItemSettings legendary;
    [SerializeField] private ItemSettings training;

    #region Stats Creation
    private String GetRandomType()
    {
        return types[GetRandomNumber(0, types.Count -1)];
    }    
    
    private String GetRandomTitle()
    {
        return titles[GetRandomNumber(0, titles.Count)];
    }

    private int GetRandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }

    private float CritMultiplier(float baseValue, float critMultiplier)
    {
        return (baseValue * critMultiplier) + baseValue;
    }

    private float OffsetFromBaseValue(float baseValue, float offset)
    {
        var rng = Random.Range(0, 10);
        
        if (rng >= 5)
            return Mathf.Abs(Random.Range(baseValue, baseValue +offset));
        
        return Mathf.Abs(Random.Range(baseValue - offset, baseValue));
    }
    #endregion

    private void Awake()
    {
        DataBaseItems = new List<InventoryItem>();
        
        for (int i = 0; i < itemsToCreate; i++)
        {
            AddItemToDatabase(i);
        }
    }
    
    public int GetDatabaseSize => DataBaseItems.Count;
    
    public InventoryItem GetItem(int id)
    {
        return DataBaseItems[id];
    }

    public InventoryItem GetItem(string itemType)
    {
        return DataBaseItems.Find(inventoryItem => inventoryItem.Type == itemType);
    }

    private InventoryItem CreateInventoryItem(int index)
    {
        var item = new InventoryItem(GetRandomType(),GetRandomNumber(0, lootRarity));
        var itemSettings = GetItemSettings(item);
        
        if (item.Type == "Sword")
        {
            item.Stats = new Dictionary<string, float>()
            {
                {"Damage", OffsetFromBaseValue(itemSettings.baseDamage, itemSettings.damageOffset)},
                {"CriticalDamage",  CritMultiplier(itemSettings.baseDamage, itemSettings.critMulti)}
            };
            
            item.ID = index;
            item.Title = GetRandomTitle();
            item.Icon = Resources.Load<Sprite>($"Items/" + item.Title + item.Type);
            item.Description = "A " + item.Title + item.Type + "!";
            
            return item;
        }
        if (item.Type == "Armor")
        {
            item.Stats = new Dictionary<string, float>()
            {
                {"CriticalChance", itemSettings.criticalChance},
                {"LifeGain", OffsetFromBaseValue(itemSettings.lifeGain, itemSettings.lifeGainOffset)}
            };
            
            item.ID = index;
            item.Title = GetRandomTitle();
            item.Icon= Resources.Load<Sprite>($"Items/" + item.Title + item.Type);
            item.Description = "A " + item.Title + item.Type + "!";
            
            return item;
        }
        return null;
    }

    public InventoryItem CreateTrainingSword()
    {
        var defaultSword = new InventoryItem(types[0], -1);
        var itemSettings = GetItemSettings(defaultSword);
        
        defaultSword.Stats = new Dictionary<string, float>()
        {
            {"Damage", OffsetFromBaseValue(itemSettings.baseDamage, itemSettings.damageOffset)},
            {"CriticalDamage", CritMultiplier(itemSettings.baseDamage, itemSettings.critMulti)},
        };
        
        defaultSword.ID = -1;
        defaultSword.Title = "Training";
        defaultSword.Icon = Resources.Load<Sprite>($"Items/" + defaultSword.Title + defaultSword.Type);
        defaultSword.Description = "A " + defaultSword.Status + defaultSword.Title + defaultSword.Type + "!";

        return defaultSword;
    }
    
    private ItemSettings GetItemSettings(InventoryItem item)
    {
        ItemSettings itemSettings;
        
        switch (item.Rarity)
        {
            case 0:
                itemSettings = common;
                item.Status = "Rusty";
                break;
            case 1:
                itemSettings = uncommon;
                item.Status = "Wooden";
                break;
            case 2:
                itemSettings = rare;
                item.Status = "Iron";
                break;
            case 3:
                itemSettings = legendary;
                item.Status = "Master";
                break;
            case -1:
                itemSettings = training;
                item.Status = "Training";
                break;
            default:
                throw new Exception("NO InventoryItem Settings available!");
        }

        return itemSettings;
    }

    private void AddItemToDatabase(int counter)
    {
        var item = CreateInventoryItem(counter);
        DataBaseItems.Add(item);
    }
}