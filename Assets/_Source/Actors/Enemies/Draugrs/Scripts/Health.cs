using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.Actors.Enemies.Draugr
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float currentHealth;
        [SerializeField] private Canvas healthBarWorldUI;
        [SerializeField] private Image healthFiller;
        [SerializeField] private bool isAlive = true;
        [SerializeField] private ParticleSystem bloodVFX;
        [SerializeField] private ParticleSystem sparksVFX;

        public static event Action EnemyWasHit; 

        public bool IsAlive => isAlive;

        private void Start()
        {
            currentHealth =  SetMaxHealth();
            isAlive = currentHealth > 0;
            InitWorldSpaceCamera();
            healthBarWorldUI.enabled = false;
            UpdateHealthBar();
        }

        private float SetMaxHealth()
        {
            var sceneCounter = FindObjectOfType<Player.Scripts.Player>().SceneCounter;
            maxHealth += sceneCounter * 10;
            
            //Debug.LogWarning("Enemy ->"+name+"<- MaxHealth is : " +maxHealth);
            return maxHealth;
        }

        private void InitWorldSpaceCamera()
        {
            healthBarWorldUI.worldCamera = Camera.main;
        }

        public void TakeDamage(float baseDamage)
        {
            var futureHealth = currentHealth - baseDamage;

            if (futureHealth > 0)
            {
                currentHealth -= baseDamage;
            }
            else if (futureHealth <= 0)
            {
                currentHealth = 0;
                isAlive = false;
            }

            EnemyWasHit?.Invoke();
            sparksVFX.Play();
            bloodVFX.Play();
            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            healthFiller.fillAmount = currentHealth / maxHealth;
        }

        private void PlayBloodVFX(Vector3 transformPosition)
        {
            bloodVFX.transform.position = transformPosition;
            bloodVFX.Play();
        }

        public void IsHealthBarVisible(bool state)
        {
            healthBarWorldUI.enabled = state;
        }
    }
}