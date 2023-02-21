using System;
using _Source.Actors.Player.Equipment;
using UnityEngine;

namespace _Source.Actors.Player.Armor
{
    public class ArmorSlot : MonoBehaviour
    {
        [SerializeField] private Armor activeArmor;

        public Armor ActiveArmor
        {
            get => activeArmor;
            set => activeArmor = value;
        }
        
        private Transform meshOriginForArmor;
    
        private void Awake()
        {
            activeArmor = GetComponentInChildren<Armor>();
            EquipmentPanel.ResetArmor += OnResetArmor;
        }

        private void OnResetArmor()
        {
            activeArmor.CriticalChance = 0f;
            activeArmor.LifeGain = 0f;

            activeArmor.gameObject.SetActive(false);
        }

        public void SetStats(InventoryItem obj)
        {
            activeArmor.gameObject.SetActive(true);
            activeArmor.CriticalChance = obj.Stats["CriticalChance"];
            activeArmor.LifeGain = obj.Stats["LifeGain"];
        }
        
        private void OnDestroy()
        {
            EquipmentPanel.ResetArmor -= OnResetArmor;
        }

    }
}