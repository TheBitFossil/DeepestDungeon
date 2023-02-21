using System;
using System.Collections.Generic;
using _Source.Inventory.Scripts;
using UnityEngine;
using UnityEngine.Playables;

namespace _Source.Actors.Player.Scripts
{
    [RequireComponent(typeof(CheckMouseLockedState))]
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private Canvas inventoryCanvas;
        [SerializeField] private Canvas healthBar;
        [SerializeField] private UIInventory uiInventory;
        [SerializeField] private bool isActive;
        [SerializeField] private PlayableDirector inventoryAnimation;
        
        private CheckMouseLockedState checkMouseState;
        public List<InventoryItem> inventoryItemsList = new List<InventoryItem>();
        
        #region properties

        public bool IsFull => uiInventory.IsFull;
        
        public bool IsActive
        {
            get => inventoryCanvas.enabled;
            set => inventoryCanvas.enabled = value;
        }

        #endregion
        
        private void Start()
        {
            checkMouseState = GetComponent<CheckMouseLockedState>();
            inventoryCanvas.enabled = false;
            checkMouseState.SetLockedToScreen(inventoryCanvas.enabled);
        }

        public void AddItem(InventoryItem item)
        {
            inventoryItemsList.Add(item);
            uiInventory.NextEmptySlot(item);
            inventoryAnimation.Play();
        }

        private InventoryItem CheckForItem(int id)
        {
            return inventoryItemsList[id];
        }

        public void RemoveItem(int id)
        {
            InventoryItem itemToRemove = CheckForItem(id);
        
            if (itemToRemove != null)
            {
                inventoryItemsList.Remove(itemToRemove);
            }
        }

        public void Toggle()
        {
            inventoryCanvas.enabled = !inventoryCanvas.enabled;
            
            healthBar.enabled = !inventoryCanvas.enabled;
            
            checkMouseState.SetLockedToScreen(inventoryCanvas.enabled);
        }
        
    }
}