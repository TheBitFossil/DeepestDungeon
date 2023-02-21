using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private AudioClip endSound;
    [SerializeField] private AudioSource audioSource;

    public void PlayAudio()
    {
        audioSource.clip = endSound;
        audioSource.Play();
    }

}