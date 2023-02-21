using UnityEngine;
using UnityEngine.UI;

namespace _Source.Actors.Player.Scripts
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private Image healthBar;
 
        public void UpdateHealthBar()
        {
            healthBar.fillAmount = health.CurrentHealth / health.MaxHealth;
        }
        
    }
}