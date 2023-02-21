using _Source.Actors.Enemies.Draugr;
using _Source.Actors.Enemies.States;
using _Source.Loot;
using Unity.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Source.Actors.Enemies.Draugrs.Scripts
{
    public class Draugr : MonoBehaviour
    {
        [SerializeField] private LootBag lootBagPrefab;
        [SerializeField] [ReadOnly] private int rngLoot;
        [SerializeField] private HealingPotion potion;
        [SerializeField] private float healingPotionDropChance = 50;
        [SerializeField] private StateMachine stateMachine;
    
        [Tooltip("Chance for Loot drop in %")] 
        [SerializeField] private int dropThreshold = 40;
    
        private DamageHandler enemyDamageHandler;
        private ItemDatabase itemDatabase;
        private Health health;
        private int dropProbability;
        private int lootDropRarity;
    
        private void Awake()
        {
            itemDatabase = FindObjectOfType<ItemDatabase>();
            enemyDamageHandler = GetComponent<DamageHandler>();
            enemyDamageHandler.StateMachine = stateMachine;
            health = GetComponent<Health>();
        
            lootDropRarity = ChooseRandomRarityForLoot();
        }

        private void Start()
        {
            if(!FindObjectOfType<ItemDatabase>())
                Debug.LogError("Please add the ItemDataBase to the Game to create Loot");
        
            rngLoot = Random.Range(0, itemDatabase.GetDatabaseSize);
        }

        private bool HasLoot()
        {
            return dropThreshold <= Random.Range(0, 100);
        }

        private void Update()
        {
            IsHealthBarVisible();
        }

        private void IsHealthBarVisible()
        {
            health.IsHealthBarVisible(enemyDamageHandler.Target);
        }

        private void TryDropLoot()
        {
            if (false == HasLoot())
            {
                TryDropHealingPotion();
                return;
            }
        
            var lootBag = Instantiate(lootBagPrefab, transform.position, Quaternion.identity);
        
            var loot = itemDatabase.DataBaseItems[rngLoot];
        
            lootBag.InventoryItem = loot;
        }

        private void TryDropHealingPotion()
        {
            var rng = Random.Range(0, 100);
            if (rng >= healingPotionDropChance)
            {
                Instantiate(potion, transform.position, Quaternion.identity);
            }
        }

        private int ChooseRandomRarityForLoot()
        {
            return Random.Range(0, itemDatabase.LootRarity);
        }

        public void DestroyAfterDeadAnimation()
        {
            TryDropLoot();
            Destroy(gameObject);
        }

        public void StopAttacking()
        {
            stateMachine.StopAttack();
        }
    }
}