using System;
using _Source.Coordinators.GameStates;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private GateExit gate;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioSource audioSource;

    private bool hasPlayed = false;
    public event Action PlayerReachedLevelEnd;
    
    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }

    private void Awake()
    {
        isActive = false;
        hasPlayed = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;
        
        if (other.CompareTag("Player"))
        {
            PlayerReachedLevelEnd?.Invoke();
        }
    }

    public void OpenGate()
    {
        gate.Unlock();
        if(!hasPlayed)
            PlaySound();
    }

    private void PlaySound()
    {
        audioSource.PlayOneShot(audioClip);
        hasPlayed = true;
    }

}