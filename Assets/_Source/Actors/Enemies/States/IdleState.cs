using UnityEngine;

namespace _Source.Actors.Enemies.States
{
    public class IdleState : BaseState, IState
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public void EnterState()
        {
            navMeshAgent.speed = agentSpeed;
            animator.SetFloat("moveSpeed", 0f);
        }

        public void UpdateState()
        {
            animator.SetBool("isAttacking", false);
            animator.SetFloat("moveSpeed", 0f);
        }
    }
}