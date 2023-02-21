using _Source.Actors.Enemies.Draugrs.Scripts;
using _Source.Actors.Enemies.States;
using _Source.Actors.Player.Scripts;
using UnityEngine;

namespace _Source.Actors.Enemies
{
    public class DamageHandler : MonoBehaviour
    {
        [SerializeField] private TargetFinder targetFinder;
        [SerializeField] private AttackArea attackArea;
        [SerializeField] private float damage = 50f;
        private StateMachine stateMachine;

        public Health Target => targetFinder.Target;
        public Health TargetHealth => attackArea.Health;

        public StateMachine StateMachine
        {
            get => stateMachine;
            set => stateMachine = value;
        }

        private void Start()
        {
            targetFinder.Radius = stateMachine.AttackRadius;
        }

        private float CalculateDamage()
        {
            var player = FindObjectOfType<Player.Scripts.Player>();
            var levelCounter = player.SceneCounter;
            var playerMaxHealth = player.Health.MaxHealth;

            var dps = damage + (levelCounter * .5f) - (playerMaxHealth / 100);
            
            return dps;
            //Debug.LogWarning(this.name + " has " +damage);
        }
        
        public void AttackTargetFromAnimationTrack()
        {
            if(attackArea.Health == null) return;
            
            attackArea.Health.TakeDamage(CalculateDamage());
            
            ForgetTarget();
        }

        public void ForgetTarget()
        {
            attackArea.Health = null;
        } 
    
    }
}