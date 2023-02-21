using System;
using System.Collections.Generic;
using _Source.Actors.Player.Equipment;
using UnityEngine;

namespace _Source.Inventory.Scripts
{
    public class UIInventory : MonoBehaviour
    {
        [Header("Slots Configuration")]
        [SerializeField] private const int maxSlots = 8;
        [SerializeField] private Transform slotPanel;
        [SerializeField] private GameObject slotPrefab;
        
        [SerializeField] private ItemDatabase itemDatabase;
        [SerializeField] private EquipmentPanel equipmentPanel;
        [SerializeField] private Actors.Player.Scripts.Inventory inventory;
        [SerializeField] private UISlot swordEquipmentSlot;
        [SerializeField] private UISlot armorEquipmentSlot;
        
        private int slotsFull;
        
        public List<UISlot> uiInventorySlot = new List<UISlot>();
        
        private UISlot equipmentSlot;
        
        public int MaxSlots => maxSlots;
        public bool IsFull => slotsFull == 8;
        
        private void Awake()
        {
            CheckItemDataBase();
            InitSlots();
        }
        
        private void Start()
        {
            var defaultItem = itemDatabase.CreateTrainingSword();
            inventory.AddItem(defaultItem);

            equipmentPanel.ResetArmorData();

            // Add the Training Sword to the Equipment Slot
            OnAddToEquipmentSlot(uiInventorySlot[0]);
        }

        private void OnDestroy()
        {
            UISlot.EquipmentSlotClicked -= OnEquipmentSlotClicked;
            UISlot.AddToEquipmentSlot -= OnAddToEquipmentSlot;
            UISlot.RemoveFromEquipmentSlot -= OnRemoveFromInventorySlot;
        }

        private void InitSlots()
        {
            UISlot.EquipmentSlotClicked += OnEquipmentSlotClicked;
            UISlot.AddToEquipmentSlot += OnAddToEquipmentSlot;
            UISlot.RemoveFromEquipmentSlot += OnRemoveFromInventorySlot;
            UISlot.RemoveItem += OnRemoveFromInventorySlot;
        }

        private void CheckItemDataBase()
        {
            if (itemDatabase == null)
                itemDatabase = FindObjectOfType<ItemDatabase>();
        }

        private void CheckSlotsContain()
        {
            var empty = 0;
            slotsFull = 0;
            
            foreach (var slot in uiInventorySlot)
            {
                if (slot.InventoryItem == null)
                {
                    empty++;
                }
                else
                {
                    slotsFull++;
                }
            }
        }


        private void OnEquipmentSlotClicked(InventoryItem obj)
        {
            switch (obj.Type)
            {
                case "Sword":
                    swordEquipmentSlot.UpdateIcon(null);
                    equipmentPanel.ResetSwordData();
                    break;
                case "Armor":
                    armorEquipmentSlot.UpdateIcon(null);
                    equipmentPanel.ResetArmorData();
                    break;
            }
            NextEmptySlot(obj);
            CheckSlotsContain();
        }
    
        private void OnAddToEquipmentSlot(UISlot inventorySlot)
        {
            // What Item do we have inside the inventory?
            switch (inventorySlot.InventoryItem.Type)
            {
                case "Sword":
                    equipmentSlot = swordEquipmentSlot;
                    break;
                case "Armor":
                    equipmentSlot = armorEquipmentSlot;
                    break;
            }

            // Clone both items
            InventoryItem itemFromEquipmentSlot;
            InventoryItem itemToEquipmentSlot;
            
            if (equipmentSlot.InventoryItem != null)
                itemFromEquipmentSlot = new InventoryItem(equipmentSlot.InventoryItem);
            else
                itemFromEquipmentSlot = null;

            if (inventorySlot.InventoryItem != null)
                itemToEquipmentSlot = new InventoryItem(inventorySlot.InventoryItem);
            else
                itemToEquipmentSlot = null;

            // Swap both inventory items
            equipmentSlot.UpdateIcon(itemToEquipmentSlot);
            inventorySlot.UpdateIcon(itemFromEquipmentSlot);
            
            // Show the item Data inside the Panel
            equipmentPanel.ReadItemData(itemToEquipmentSlot);
            CheckSlotsContain();
        }

        private void OnRemoveFromInventorySlot(UISlot inventorySlot)
        {
            if (inventorySlot.InventoryItem == null) { return; }
            if (inventorySlot.InventoryItem.Rarity == -1) { return; }

            // Slot does has no inventory icon left
            inventorySlot.UpdateIcon(null);
            // Update the whole slots to show the corresponding icons
            foreach (var slot in uiInventorySlot)
            {
                slot.UpdateIcon(slot.InventoryItem);
            }
            
            if(inventorySlot.InventoryItem != null)
                // Find the inventory item with ID and remove it from database
                inventory.RemoveItem(inventorySlot.InventoryItem.ID);

            CheckSlotsContain();
        }
        
        
        private void UpdateSlot(int slot, InventoryItem item)
        {
            uiInventorySlot[slot].UpdateIcon(item);
        }

        public void NextEmptySlot(InventoryItem item)
        {
            var index = 0;
        
            foreach (var slot in uiInventorySlot)
            {
                if (slot.InventoryItem == null)
                {
                    UpdateSlot(index, item);
                    break;
                }
                index++;
            }
            
            CheckSlotsContain();
        }

        public void RemoveItem(InventoryItem item)
        {
            UpdateSlot(uiInventorySlot.FindIndex(i=> i.InventoryItem == item), null);
            CheckSlotsContain();
        }

    }
}