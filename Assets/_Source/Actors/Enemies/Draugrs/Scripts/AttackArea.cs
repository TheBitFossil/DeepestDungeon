using System;
using _Source.Actors.Player.Scripts;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Health health;

    public Health Health
    {
        get => health;
        set => health = value;
    }
    
    public event Action<Health> TargetLock;

    public void ToggleHitBoxFromAnimation(bool state)
    {
        switch (state)
        {
            case true:
                boxCollider.enabled = true;
                break;
            case false:
                boxCollider.enabled = false;
                break;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            health = other.gameObject.GetComponent<Health>();
            TargetLock?.Invoke(health);
        }
    }
    
}