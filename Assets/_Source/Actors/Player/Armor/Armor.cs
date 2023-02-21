
using System;
using UnityEngine;

namespace _Source.Actors.Player.Armor
{
    public class Armor : MonoBehaviour
    {
        private static float criticalChance;
        private static float lifeGain;

        #region InputData from Item
        public float CriticalChance
        {
            get => criticalChance;
            set => criticalChance = value;
        }
        
        public float LifeGain
        {
            get => lifeGain;
            set => lifeGain = value;
        }
        #endregion

        #region Output Data for Calculations
        // Health reads this
        public static float CriticalChanceOut => criticalChance;
        public static float LifeGainOut => lifeGain;
        #endregion

    }
}