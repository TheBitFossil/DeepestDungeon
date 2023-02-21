using System;
using UnityEngine;

namespace DataBase
{
    [Serializable]
    public class ItemSettings 
    { 
        [SerializeField] [Tooltip("Range from BaseDamage +/-")] 
        public float baseDamage = 25f;
        
        [SerializeField] [Tooltip("Set Value: (DPS * Multi) + DPS")] 
        public float damageOffset = 9f;
        
        [SerializeField] [Tooltip("Set BaseValue")]
        public float critMulti = .35f;
        
        [SerializeField] [Tooltip("Potion value, for Healing")]
        public float lifeGain = 50f;

        [SerializeField] [Tooltip("Potion value, for Healing")]
        public float lifeGainOffset = 2f;
        
        [SerializeField] [Tooltip("Critical Hit Chance %")]
        public float criticalChance = .1f;
    }
}