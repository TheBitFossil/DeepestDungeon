using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{
    public UnityEvent AnimationAttackAction;
    public UnityEvent ResumePlayerMovement;
    public UnityEvent AnimationEnded;
    public UnityEvent FeetSoundEffect;
    public UnityEvent LightAttackSound;
    public UnityEvent HeavyAttackSound;
    public UnityEvent HitBoxOn;
    public UnityEvent HitBoxOff;
    public UnityEvent LookAtOn;
    public UnityEvent LookAtOff;
    public UnityEvent EnemyIsDead;
    public UnityEvent Voice;
    public UnityEvent HeavyVoice;

    public void AttackAction()
    {
        AnimationAttackAction?.Invoke();
    }

    public void PlayerMovement()
    {
        ResumePlayerMovement?.Invoke();
    }

    public void AnimationEnd()
    {
        AnimationEnded?.Invoke();
    }

    public void HitBoxToggleOn()
    {
        HitBoxOn?.Invoke();
    }

    public void LookAtSetOn()
    {
        LookAtOn?.Invoke();
    }
    
    public void LookAtSetOff()
    {
        LookAtOff?.Invoke();
    }
    
    public void HitBoxToggleOff()
    {
        HitBoxOff?.Invoke();
    }
    
    public void LightAttacks()
    {
        LightAttackSound?.Invoke();
    }

    public void EnemyDead()
    {
        EnemyIsDead?.Invoke();
    }
    
    public void Voices()
    {
        Voice?.Invoke();
    }

    public void HeavyVoices()
    {
        HeavyVoice?.Invoke();
    }
    
    public void HeavyAttacks()
    {
        HeavyAttackSound?.Invoke();
    }
    
    public void FeetIKSound()
    {
        FeetSoundEffect?.Invoke();
    }

}