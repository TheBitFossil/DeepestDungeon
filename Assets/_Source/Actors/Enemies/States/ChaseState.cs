using UnityEngine;
using _Source.Actors.Player.Scripts;

namespace _Source.Actors.Enemies.States
{
    public class ChaseState : BaseState, IState
    {
        public void EnterState()
        {
            animator.SetBool("isAttacking", false);
            navMeshAgent.speed = agentSpeed;
        }

        public void UpdateState()
        {
            if(Player.Scripts.Player.PlayerInstance.transform.position != null)
                navMeshAgent.SetDestination(Player.Scripts.Player.PlayerInstance.transform.position);
            
            animator.SetFloat("moveSpeed", navMeshAgent.velocity.magnitude / navMeshAgent.speed);
        }
 
    }
}