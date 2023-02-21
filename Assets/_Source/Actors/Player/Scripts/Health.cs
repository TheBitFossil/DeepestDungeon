using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Playables;
using Random = UnityEngine.Random;

namespace _Source.Actors.Player.Scripts
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private HealthBar healthBar;
        [SerializeField] [Tooltip("HealthBar Director")] private PlayableDirector director;

        [Header("Health")]
        [SerializeField] private float maxHitPoints = 100f;
        [SerializeField] [ReadOnly] private float currentHealth;
            
        [Header("VFX")] 
        [SerializeField] private Animator animator;
        [SerializeField] private ParticleSystem healthGain;
        [SerializeField] private ParticleSystem bloodVFX;
        
        [Header("Loading Screen Toggle Canvas")] [SerializeField]
        private Canvas healthCanvas;
        
        [Header("Sound Effects")]
        [SerializeField] private AudioSource audioSourceHurt;
        [SerializeField] private List<AudioClip> hurtSounds;
        [SerializeField] private float minPitch = .8f;
        [SerializeField] private float maxPitch = 1f;
        
        public bool IsAlive => currentHealth > 0;
        public float CurrentHealth => currentHealth;
        public float MaxHealth => maxHitPoints + Armor.Armor.LifeGainOut;
        
        public event Action PlayerDespawned;
        public event Action PlayerDespawning;
        
        private void Awake()
        {
            currentHealth = MaxHealth;
            healthBar.UpdateHealthBar();
        }

        public void TakeDamage(float amount)
        {
            var futureHealth = currentHealth - amount;
                            
            if (futureHealth <= 0)
            {
                currentHealth = 0;
                animator.SetBool("isDead", true);
                PlayerDespawning?.Invoke();
            }
            else
            {
                currentHealth -= amount;
                director.Play();
                animator.Play("HitAnim");
                bloodVFX.Play();
            }
            
            PlayHurtSound();
            healthBar.UpdateHealthBar();
        }
            
        public void AddHealth(int amount, HealingPotion potion)
        {
            if(currentHealth >= MaxHealth) { return; }
            
            var futureHealth = currentHealth + amount;

            if (futureHealth >= MaxHealth)
                currentHealth = MaxHealth;
            else
                currentHealth += amount;
            
            healthGain.Play();
            potion.ClearItem();
            healthBar.UpdateHealthBar();
        }

        private void PlayHurtSound()
        {
            var rng = Random.Range(0, hurtSounds.Count);

            audioSourceHurt.pitch = Random.Range(minPitch, maxPitch);
            AudioSource.PlayClipAtPoint(hurtSounds[rng], transform.position);
        }
        
        public void DespawnAfterDeathAnimation()
        {
            PlayerDespawned?.Invoke();
        }

        public void SetHealthBar(bool state)
        {
            healthCanvas.enabled = state;
        }
    }
}