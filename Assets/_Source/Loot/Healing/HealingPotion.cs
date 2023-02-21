using System;
using System.Collections;
using System.Collections.Generic;
using _Source.Actors.Player.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class HealingPotion : MonoBehaviour
{
    [SerializeField] private int healingAmount = 40;
    [SerializeField] private List<AudioClip> healSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health>().AddHealth(healingAmount, this);
           
        }
    }

    private void PlayHealingSound()
    {
        var rng = Random.Range(0, healSound.Count);
        AudioSource.PlayClipAtPoint(healSound[rng], transform.position);        
    }

    public void ClearItem()
    {
        PlayHealingSound();
        Destroy(gameObject);
    }
}