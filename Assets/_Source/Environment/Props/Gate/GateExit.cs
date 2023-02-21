using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateExit : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void Unlock()
    {
        animator.SetBool("unlocked", true);
    }
}