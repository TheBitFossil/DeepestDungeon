using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = System.Object;

namespace _Source.Inventory.Scripts
{
    public class UISlot : MonoBehaviour, IPointerClickHandler
    {
        [Header("Default Sprites")] [SerializeField]
        private Sprite defaultFill;

        [SerializeField] private Sprite defaultIcon;

        [Header("Object location")] [SerializeField]
        private Image fillImage;

        [SerializeField] 
        private Image iconImage;

        [Header("Rarity Backgrounds")] [SerializeField]
        private Sprite[] raritySprites;

        [SerializeField] private bool isEquipSlot;

        #region Properties
        private InventoryItem item;
        public InventoryItem InventoryItem => item;
        public bool IsEquipSlot
        {
            get => isEquipSlot;
            set => value = isEquipSlot;
        }
        #endregion

        #region Events
        public static event Action<InventoryItem> EquipmentSlotClicked;
        public static event Action<UISlot> AddToEquipmentSlot;
        public static event Action<UISlot> RemoveFromEquipmentSlot; 
        public static event Action<UISlot> RemoveItem; 
        #endregion
        
        private void Awake() { UpdateIcon(item); }
        
        public void UpdateIcon(InventoryItem inventoryItem)
        {
            if (inventoryItem == null)
            {
                fillImage.enabled = false;
                iconImage.sprite = defaultIcon;
            }
            else
            {
                // If we have the training sword equipped
                if (inventoryItem.Rarity == -1)
                {
                    fillImage.sprite = raritySprites[0];
                    fillImage.enabled = true;
                    iconImage.sprite = inventoryItem.Icon;
                }
                else
                {
                    fillImage.sprite = raritySprites[inventoryItem.Rarity];
                    fillImage.enabled = true;
                    iconImage.sprite = inventoryItem.Icon;
                }
            }

            item = inventoryItem;
        }

        // Epic exist function made by HannaZ.
        bool Exist(Object ob)
        {
            switch (ob)
            {
                case null: return false;
                default: return true;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isEquipSlot)
            {
                EquipmentSlotClicked?.Invoke(item);
            }
            else
            {
                if (Exist(item) && eventData.button == PointerEventData.InputButton.Left)
                {
                    // Give whole slot to inventory, for cloning item ( swapping )
                    AddToEquipmentSlot?.Invoke(this);
                }
                else if (Exist(item) && eventData.button == PointerEventData.InputButton.Right)
                {
                    // Update the Slot with null and remove item
                    RemoveItem?.Invoke(this);
                    
                }
                else if (Exist(item) && eventData.button == PointerEventData.InputButton.Middle)
                {
                    RemoveFromEquipmentSlot?.Invoke(this);
                }
                else if(!Exist(item))
                {
                    //Debug.LogWarning("No Item in slot!!");
                }
            }
        }
        
    }
}