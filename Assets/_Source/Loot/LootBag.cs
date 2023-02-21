using System;
using System.Collections.Generic;
using _Source.Actors.Player.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Source.Loot
{
    public class LootBag : MonoBehaviour
    {
        [SerializeField] private Animator animatorLid;
        [SerializeField] private Animator animatorLock;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip lootCollectSound;
        [SerializeField] private List<AudioClip> lootDropSound;
        [SerializeField] private List<GameObject> vfxOpen;
        [SerializeField] private List<GameObject> vfxClosed;
        [SerializeField] private List<ParticleSystem> vfxPickUp;

        public InventoryItem InventoryItem;

        private void Start()
        {
            AudioSource.PlayClipAtPoint(lootDropSound[InventoryItem.Rarity], transform.position);
            
            vfxClosed[InventoryItem.Rarity].SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var player = other.GetComponent<Player>();

                if (player.CanLoot && !player.InventoryIsFull)
                {
                    var collector = other.gameObject.GetComponent<Actors.Player.Scripts.Inventory>();
                    collector.AddItem(InventoryItem);

                    audioSource.PlayOneShot(lootCollectSound);
                    
                    vfxPickUp[InventoryItem.Rarity].gameObject.transform.SetParent(other.transform, false);
                    vfxPickUp[InventoryItem.Rarity].gameObject.SetActive(true);
                    vfxPickUp[InventoryItem.Rarity].Play();
                    
                    Destroy(gameObject);
                }
            }
        }

        public void Activate()
        {
            vfxClosed[InventoryItem.Rarity].SetActive(false);
            vfxOpen[InventoryItem.Rarity].SetActive(true);

            animatorLid.Play("OpenLid");
            animatorLock.Play("OpenSegment");
        }

        private void OnDestroy()
        {
            vfxOpen[InventoryItem.Rarity].SetActive(false);
        }
    }
}