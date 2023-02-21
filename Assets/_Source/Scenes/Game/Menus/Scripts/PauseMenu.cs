using System;
using _Source.Coordinators;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.Scenes.Game.Menus.Scripts
{
    [RequireComponent(typeof(CheckMouseLockedState))]
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        private SceneLoader sceneLoader;
        private CheckMouseLockedState checkMouseLockedState;

        public bool IsActive => panel!= null? panel.activeInHierarchy : false;
        public event Action PauseMenuResumeGame;
        public event Action PauseMenuLoadMainMenu;
        public event Action PauseMenuExitApp;
        
        private void Start()
        {
            checkMouseLockedState = GetComponent<CheckMouseLockedState>();
            sceneLoader = FindObjectOfType<SceneLoader>();
        }

        public void Toggle()
        {
            if (panel.gameObject.activeInHierarchy)
                panel.gameObject.SetActive(false);
            else
                panel.gameObject.SetActive(true);
            
            checkMouseLockedState.SetLockedToScreen(panel.activeInHierarchy);
        }

        public void ButtonResumeGame()
        {
            PauseMenuResumeGame?.Invoke();
            Toggle();
        }
        
        
        public void ButtonBackToMainMenu()
        {
            PauseMenuLoadMainMenu?.Invoke();
        }
        
        
        public void ButtonExitApplication()
        {
            PauseMenuExitApp?.Invoke();
        }
    }
}