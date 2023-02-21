using System;
using System.Collections;
using System.Collections.Generic;
using _Source.Actors.Enemies.Draugr;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Android;

namespace _Source.Actors.Player.Weapons
{
    public class Weapon : MonoBehaviour
    {
      [SerializeField] private HitBox hitBox;
      [SerializeField] private Animator animator;
      [SerializeField] private ParticleSystem swordTrailVFX;
      [SerializeField] private DamageDealer damageDealer;
      [SerializeField] private WeaponSounds weaponSounds;
      
      [Header("Attack Configuration")]
      [SerializeField] [ReadOnly] private float attackRate = .45f; 
      [SerializeField] private bool canAttack = true;
      [SerializeField] private int attackHits;
      [SerializeField] private float baseAttackDmgMultiplier = 1f;
      [SerializeField] private float heavyAttackDmgMultiplier = 1.5f;
      
      private string currentAnimBase;
      private string currentAnimHeavy;
      private string lastAnimBase;
      private string lastAnimHeavy;
      
      public bool CanAttack
      {
         get => canAttack;
         set => canAttack = value;
      }

      public HitBox HitBox
      {
         get => hitBox;
         set => hitBox = value;
      }

      public WeaponSounds WeaponSounds
      {
         get => weaponSounds;
         set => weaponSounds = value;
      }

      #region Attack HashStrings
      private static readonly int IdleAttack01 = Animator.StringToHash("IdleAttack_01");
      
      private static readonly int LightAttack02 = Animator.StringToHash("LightAttack_02");
      private static readonly int LightAttack03 = Animator.StringToHash("LightAttack_03");
      
      private static readonly int MoveHeavyAttack01 = Animator.StringToHash("MoveHeavyAttack_01");
      private static readonly int MoveHeavyAttack02 = Animator.StringToHash("MoveHeavyAttack_02");
      
      private static readonly int AttackHits = Animator.StringToHash("attackHits");
      private static readonly int HeavyAttackHits = Animator.StringToHash("heavyAttackHits");
      #endregion

      private void Start()
      {
         animator = GetComponentInParent<Animator>();
         damageDealer = GetComponent<DamageDealer>();
         damageDealer.CriticalHitDamage += OnCriticalHit;
         hitBox = GetComponentInChildren<HitBox>();
         weaponSounds = GetComponent<WeaponSounds>();
         hitBox.HitBoxTarget += OnHitBoxTarget;
      }

      public void BaseAttack()
      {
         if (false == canAttack) return;
         StartCoroutine(ResetAttackTimer());
         
         currentAnimBase = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
         
         if (lastAnimBase != currentAnimBase || attackHits == 0)
         {
            attackHits = animator.GetInteger(AttackHits);
            
            animator.SetInteger(AttackHits, attackHits+1);
            
            damageDealer.AttackDmgMultiplier = baseAttackDmgMultiplier;
            
            lastAnimBase = currentAnimBase;
         }
      }

      public void HeavyAttack()
      {
         if (false == canAttack) return;
         StartCoroutine(ResetAttackTimer());
         
         swordTrailVFX.Play();
         currentAnimHeavy = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
         
         if (lastAnimHeavy != currentAnimHeavy || attackHits == 0)
         {
            var heavyAttackHits = animator.GetInteger(HeavyAttackHits);
            
            animator.SetInteger(HeavyAttackHits,heavyAttackHits+1);
            damageDealer.AttackDmgMultiplier = heavyAttackDmgMultiplier;

            lastAnimHeavy = currentAnimHeavy;
         }
      }

      IEnumerator ResetAttackTimer()
      { 
         canAttack = false;
         swordTrailVFX.Stop();
         yield return new WaitForSeconds(attackRate);
         canAttack = true;
         swordTrailVFX.Play();
      }

      private void OnHitBoxTarget(GameObject obj)
      {
         if (obj.TryGetComponent<Health>(out Health enemyHealth))
         {
            damageDealer.DamageCalculationOnTarget(enemyHealth);
         }
      }
      
      private void OnCriticalHit()
      {
        // weaponSounds.PlayCritSound();
        // TODO: Play Critical Hit sound ?!
      }
      
      private void OnDestroy()
      {
         damageDealer.CriticalHitDamage -= OnCriticalHit;
         hitBox.HitBoxTarget -= OnHitBoxTarget;
      }
    }
}