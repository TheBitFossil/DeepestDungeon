using System;
using UnityEngine;
using UnityEngine.AI;

namespace _Source.Actors.Enemies.States
{
    public class BaseState : MonoBehaviour
    {
        [SerializeField] protected NavMeshAgent navMeshAgent;
        [SerializeField] protected Animator animator;
        [SerializeField] protected float agentSpeed;

        protected virtual void Awake()
        {
            if (navMeshAgent == null)
            {
                navMeshAgent = GetComponent<NavMeshAgent>();
            }
        }
    }
}