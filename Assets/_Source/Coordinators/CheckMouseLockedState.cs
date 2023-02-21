using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMouseLockedState : MonoBehaviour
{
    public void SetLockedToScreen(bool state)
    {
        switch (state)
        {
            case true:
                Cursor.lockState = CursorLockMode.Confined;
                break;
            case false:
                Cursor.lockState = CursorLockMode.Locked;
                break;
        }
    }
    
}