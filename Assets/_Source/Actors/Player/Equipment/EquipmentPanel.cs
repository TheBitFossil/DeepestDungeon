using System;
using System.Collections.Generic;
using _Source.Actors.Player.Armor;
using UnityEngine;

namespace _Source.Actors.Player.Equipment
{
    public class EquipmentPanel : MonoBehaviour
    {
        [SerializeField] private List<StatSlot> weaponStatSlots;
        [SerializeField] private List<StatSlot> armorStatSlots;
        [SerializeField] private WeaponSlot weaponSlot;
        [SerializeField] private ArmorSlot armorSlot;
        
        public static event Action ResetSword;
        public static event Action ResetArmor;
        
        public void ReadItemData(InventoryItem obj)
        {
            switch (obj.Type)
            {
                case "Sword":
                    UpdateSlots(0, obj.Stats["Damage"], true);
                    UpdateSlots(1, obj.Stats["CriticalDamage"], true);
                    UpdateSlots(2, obj.Title, true);
                    UpdateSlots(3, obj.Status, true);
                    
                    weaponSlot.SetStats(obj);
                    break;
                case "Armor":
                    UpdateSlots(0, obj.Stats["CriticalChance"], false);
                    UpdateSlots(1, obj.Stats["LifeGain"], false);
                    UpdateSlots(2, obj.Title, false);
                    UpdateSlots(3, obj.Status, false);
                    
                    armorSlot.SetStats(obj);
                    break;
            }
        }

        public void ResetSwordData()
        {
            UpdateSlots(0, 0, true);
            UpdateSlots(1, 0, true);
            UpdateSlots(2, 0, true);
            UpdateSlots(3, 0, true);
            
            ResetSword?.Invoke();
        }
        
        public void ResetArmorData()
        {
            UpdateSlots(0, 0, false);
            UpdateSlots(1, 0, false);
            UpdateSlots(2, 0, false);
            UpdateSlots(3, 0, false);
            
            ResetArmor?.Invoke();
        }
        
        private void UpdateSlots(int num, string objTitle, bool weapon)
        {
            if(weapon)
                weaponStatSlots[num].UpdateSlot(objTitle);
            else
                armorStatSlots[num].UpdateSlot(objTitle);
        }

        private void UpdateSlots(int num, float value, bool weapon)
        {
            if(weapon)
                weaponStatSlots[num].UpdateSlot(value);
            else
                armorStatSlots[num].UpdateSlot(value);
        } 
    
    }
}