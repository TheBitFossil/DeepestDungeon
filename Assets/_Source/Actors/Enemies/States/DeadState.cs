using UnityEngine;

namespace _Source.Actors.Enemies.States
{
    public class DeadState : BaseState, IState
    {
        public void EnterState()
        {
            animator.SetBool("isDead", true);
            navMeshAgent.speed = agentSpeed;
        }

        public void UpdateState()
        {
            animator.SetFloat("moveSpeed", 0f);
        }
    }
}