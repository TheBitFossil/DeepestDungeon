using System;
using _Source.Actors.Enemies.Draugr;
using _Source.Coordinators.GameStates;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private BoxCollider hitBoxCollider;
       
    public event Action<GameObject> HitBoxTarget;
    
    private void Start()
    {
        hitBoxCollider = GetComponent<BoxCollider>();
        hitBoxCollider.isTrigger = true;
    }

    public void ToggleHitBoxFromAnimation(bool state)
    {
        switch (state)
        {
            case true:
                hitBoxCollider.enabled = true;
                break;
            case false:
                hitBoxCollider.enabled = false;
                break;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attackable"))
        {
            HitBoxTarget?.Invoke(other.gameObject);
            if (hitBoxCollider.enabled)
                hitBoxCollider.enabled = false;
        }
    }

}