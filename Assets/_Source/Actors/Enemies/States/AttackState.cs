using System;
using System.Collections;
using _Source.Actors.Enemies.Draugr;
using UnityEngine;

namespace _Source.Actors.Enemies.States
{
    public class AttackState : BaseState, IState
    {
       [SerializeField] private DamageHandler damageHandler;
       [SerializeField] private float attackRate;
       [SerializeField] private bool canAttack;

       private static readonly int MoveSpeed = Animator.StringToHash("moveSpeed");

       public bool CanAttack
       {
           get => canAttack;
           set => canAttack = value;
       }

       private void Start()
       {
           Health.EnemyWasHit += OnAttackHit;
       }
       
       private void OnDestroy()
       {
           Health.EnemyWasHit -= OnAttackHit;
       }

        public void EnterState()
       {
            navMeshAgent.speed = agentSpeed;
            animator.SetFloat(MoveSpeed, 0f);

            if (canAttack)
            {
                animator.Play("Attack");
                canAttack = false;
            }
       }

        public void UpdateState()
        {
            if (canAttack)
            {
                // Attack Target
                animator.Play("Attack");
                canAttack = false;
            }
        }

        public void AttackAnimationEnded()
        {
           
            StartCoroutine(AttackCooldown());
        }

        private IEnumerator AttackCooldown()
        {
            yield return new WaitForSeconds(attackRate);
            canAttack = true;
        }

        private void OnAttackHit()
        {
            var currentAttackAnim = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
           
            if (currentAttackAnim != "Attack" && canAttack)
            {
                animator.Play("Attack");
            }
        }
    }
}