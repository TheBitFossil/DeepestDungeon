    using _Source.Actors.Enemies.Draugrs.Scripts;
    using UnityEngine;

namespace _Source.Actors.Enemies
{
    public class LookAt : MonoBehaviour
    {
        [SerializeField] private TargetFinder targetFinder;
        [SerializeField] private bool isActive;
        private GameObject target;

        public bool IsActive
        {
            get => isActive;
            set => isActive = value;
        }

        private void FixedUpdate()
        {
            if (false == isActive) { return; }

            if (targetFinder.Target != null)
            {
                FaceDirection(targetFinder.Target.gameObject);
            }
        }

        private void FaceDirection(GameObject go)
        {
            Vector3 relativePosition = go.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePosition);

            Quaternion current = transform.localRotation;
            
            transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime * 5f);
            
            
            //transform.LookAt(go.transform, Vector3.up);
        }
        
    }
}