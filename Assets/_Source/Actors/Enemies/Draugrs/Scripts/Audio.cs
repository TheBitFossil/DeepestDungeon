using System.Collections.Generic;
using UnityEngine;

namespace _Source.Actors.Enemies.Draugrs.Scripts
{
    public class Audio : MonoBehaviour
    {
        [Header("Sound Effects")] 
        [SerializeField] private AudioSource audioSourceWalk;
        [SerializeField] private AudioSource audioSourceAttack;
        [SerializeField] private AudioSource audioSourceVoices;
        [SerializeField] private List<AudioClip> walkSounds;
        [SerializeField] private List<AudioClip> attackSounds;
        [SerializeField] private List<AudioClip> voices;
        [SerializeField] private float minPitch = .8f;
        [SerializeField] private float maxPitch = 1f;
    
        public void PlayWalkSound()
        {
            var rng = Random.Range(0, walkSounds.Count);

            audioSourceWalk.pitch = Random.Range(minPitch, maxPitch);
            AudioSource.PlayClipAtPoint(walkSounds[rng], transform.position);
        }
        
        public void PlayAttackSound()
        {
            var rng = Random.Range(0, attackSounds.Count);

            audioSourceAttack.pitch = Random.Range(minPitch, maxPitch);
            AudioSource.PlayClipAtPoint(attackSounds[rng], transform.position);
        }
        
        public void PlayVoices()
        {
            var rng = Random.Range(0, voices.Count);

            audioSourceVoices.pitch = Random.Range(minPitch, maxPitch);
            AudioSource.PlayClipAtPoint(voices[rng], transform.position);
        }
    }
}