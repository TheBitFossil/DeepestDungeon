using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook cmCamera;
    [SerializeField] private CinemachineBasicMultiChannelPerlin cmBasicMultiChannelPerlin;
    [SerializeField] private float shakeTimer = 1f;
    
    public static CameraShake Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cmBasicMultiChannelPerlin = cmCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        
        if(cmBasicMultiChannelPerlin == null)
            Debug.LogWarning("No MultiChannelPerlin on the virtual camera.", this);
        else
            Debug.Log($"Noise Component: {cmBasicMultiChannelPerlin}");
    }

    public void Shake(float intensity, float time)
    {
        cmBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        //Using Input System fehlt noch
        // gamepad.SetMotorSpeeds(Werty, Wertx);
        //kannst noch zwei werte hinzufgen fr das
        //so geht das glaube ich. Kannst du noch machen wenn du magst :)

        shakeTimer = time;
    }

    private void Update()
    {
        shakeTimer -= Time.deltaTime;

        if (shakeTimer > 0)
        {
            if (shakeTimer <= 0f)
            {
                cmBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}