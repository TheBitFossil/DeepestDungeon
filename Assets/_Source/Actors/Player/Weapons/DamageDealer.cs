using System;
using _Source.Actors.Enemies.Draugr;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Source.Actors.Player.Weapons
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField] private float baseDamage;
        [SerializeField] private float critDamage;
        
        private float attackDmgMultiplier;
        public event Action CriticalHitDamage;

        public float BaseDamage
        {
            get => baseDamage;
            set => baseDamage = value;
        }
    
        public float AttackDmgMultiplier
        {
            get => attackDmgMultiplier;
            set => attackDmgMultiplier = value;
        }
    
        public float CritDamage
        {
            get => critDamage;
            set => critDamage = value;
        }

        private bool IsCriticalHit()
        {
            var rng = Random.Range(0, 100);
            var cc = Armor.Armor.CriticalChanceOut;

            if (cc <= 0)
            {
                cc = 0;
                if (cc >= rng)
                {
                    Debug.LogWarning("RNG is: " +rng);
                    return true;
                }
                return false;
            }

            return false;
        }
        
        public void DamageCalculationOnTarget(Health draugr)
        {
            var dmg = 0f;
            if (IsCriticalHit())
            {
                dmg = critDamage;
                CriticalHitDamage?.Invoke();
            }
            else
            {
                dmg = baseDamage;
            }

            dmg += attackDmgMultiplier;
            draugr.TakeDamage(dmg);
        }
    }
}