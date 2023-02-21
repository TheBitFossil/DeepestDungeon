using System;
using System.Collections;
using System.Collections.Generic;
using _Source.Coordinators;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class TutorialObject : MonoBehaviour
{
    [SerializeField] private UIController uiController;
    [SerializeField] private Canvas canvas;
    [SerializeField] private PlayableDirector director;

    private void OnHideTutorialObject()
    {
        SetEnabled(false);
    }

    private void Update()
    {
        if (uiController.IsInventoryActive || uiController.IsPauseMenuActive)
        {
            director.Pause();
            SetEnabled(false);
        }
        else
        {
            director.Resume();
            SetEnabled(true);
        }
    }
    
    public void SetEnabled(bool state)
    {
        canvas.enabled = state;
    }
    
}