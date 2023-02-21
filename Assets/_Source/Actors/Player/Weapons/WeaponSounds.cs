using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Source.Actors.Player.Weapons
{
    public class WeaponSounds : MonoBehaviour
    {
        [Header("Sound Effects")]
        [SerializeField] private List<AudioClip> lightAttacks;
        [SerializeField] private List<AudioClip> heavyAttacks;
        [SerializeField] private List<AudioClip> voices;
        [SerializeField] private List<AudioClip> heavyVoices;

        [SerializeField] private AudioSource audioSourceAttacks;
        [SerializeField] private AudioSource audioSourceVoices;
        [SerializeField] private AudioSource audioSourceHeavyVoices;
        
        [SerializeField] private float minPitch;
        [SerializeField] private float maxPitch;

        private void Awake()
        {
            audioSourceAttacks.playOnAwake = false;
        }
        
        private AudioClip GetLightAttackSound()
        {
            var rng = Random.Range(0, lightAttacks.Count);
            return lightAttacks[rng];
        }
        
        private AudioClip GetHeavyAttackSound()
        {
            var rng = Random.Range(0, heavyAttacks.Count);
            return heavyAttacks[rng];
        }

        public void PlayAttackSoundFromAnimation(int state)
        {
            AudioClip nextClip = null;

            switch (state)
            {
               case 0:
                   nextClip = GetLightAttackSound();
                   break;
               case 1:
                   nextClip = GetHeavyAttackSound();
                   break;
            }
            
            audioSourceAttacks.pitch = Random.Range(minPitch, maxPitch);
            audioSourceAttacks.clip = nextClip;
           
            audioSourceAttacks.Play();
        }
        
        public void PlayVoice()
        {
            AudioClip nextClip = null;
            
            var rng = Random.Range(0, voices.Count);
            nextClip = voices[rng];
            
            audioSourceVoices.clip = nextClip;
            audioSourceVoices.pitch = Random.Range(minPitch, maxPitch);
            
            audioSourceVoices.Play();
        }
        
        public void PlayHeavyVoice()
        {
            AudioClip nextClip = null;
            
            var rng = Random.Range(0, voices.Count);
            nextClip = voices[rng];
            
            audioSourceHeavyVoices.clip = nextClip;
            audioSourceHeavyVoices.pitch = Random.Range(minPitch, maxPitch);
            
            audioSourceHeavyVoices.Play();
        }
    }
}