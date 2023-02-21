using _Source.Actors.Player.Scripts;
using UnityEngine;

namespace _Source.Actors.Enemies.Draugrs.Scripts
{
    public class TargetFinder : MonoBehaviour
    {
        [SerializeField] private SphereCollider sphereCollider;
        [SerializeField] private Health health;
        [SerializeField] private bool isActive;

        public bool IsActive => isActive;
        public Health Target => health;
    
        public float Radius
        {
            get => sphereCollider.radius;
            set => sphereCollider.radius = value;
        }
    
        public void SetColliderEnabled(bool state)
        {
            sphereCollider.enabled = state;
            isActive = state;
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if(false == isActive) {return;}

            if (other.CompareTag("Player"))
            {
                health = other.gameObject.GetComponent<Player.Scripts.Player>().GetComponent<Health>();
            }
        }
    }
}