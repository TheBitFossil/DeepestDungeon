using _Source.Actors.Enemies.Draugr;
using UnityEngine;

namespace _Source.Actors.Enemies.States
{
    public class StateMachine : MonoBehaviour
    {
        public enum AIState { Idle, Chase, Attack, Dead }

        [SerializeField] private AIState initState;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private float chaseRadius;
        [SerializeField] private float attackRadius;
        [SerializeField] [Tooltip("Draugr Health!")] private Health health;
        
        public float ChaseRadius
        {
            get => chaseRadius;
            set => chaseRadius = value;
        }

        public float AttackRadius
        {
            get => attackRadius;
            set => attackRadius = value;
        }

        private IState currentState;
        private AIState aiState;

        private IdleState idleState;
        private ChaseState chaseState;
        private AttackState attackState;
        private DeadState deadState;
        
        private void Start()
        {
            idleState = GetComponent<IdleState>();
            chaseState = GetComponent<ChaseState>();
            attackState = GetComponent<AttackState>();
            deadState = GetComponent<DeadState>();
            
            aiState = initState;
            EnterState();
        }

        private void Update()
        {
            EvaluateState();
            currentState.UpdateState();
        }

        private void EvaluateState()
        {
            bool isPlayerInChaseRange =
                Physics.CheckSphere(transform.position, chaseRadius, playerLayer);
            
            bool isPlayerInAttackRange =
                Physics.CheckSphere(transform.position, attackRadius, playerLayer);

            bool notIdle = aiState != AIState.Idle;
            bool notChasing = aiState != AIState.Chase;
            bool notAttacking = aiState != AIState.Attack;
            bool isDead = aiState == AIState.Dead;

            if (health.IsAlive && false == isDead)
            {
                if (notIdle && !isPlayerInChaseRange && !isPlayerInAttackRange)
                {
                    aiState = AIState.Idle;
                    EnterState();
                }
                else if (notChasing && isPlayerInChaseRange && !isPlayerInAttackRange)
                {
                    aiState = AIState.Chase;
                    EnterState();
                }
                else if (notAttacking && isPlayerInChaseRange && isPlayerInAttackRange)
                {
                    aiState = AIState.Attack;
                    EnterState();
                } 
            }
            else if(false == health.IsAlive)
            {
                aiState = AIState.Dead;
                EnterState();
            }
        }

        private void EnterState()
        {
            switch (aiState)
            {
                case AIState.Idle:
                    currentState = idleState;
                    break;
                case AIState.Chase:
                    currentState = chaseState;
                    break;
                case AIState.Attack:
                    currentState = attackState;
                    break;
                case AIState.Dead:
                    currentState = deadState;
                    break;
            }
            
            currentState.EnterState();
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, chaseRadius);
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRadius);
        }

        public void StopAttack()
        {
            currentState = idleState;
        }
    }
}