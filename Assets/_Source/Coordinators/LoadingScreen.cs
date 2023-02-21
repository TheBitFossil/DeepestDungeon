using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Slider = UnityEngine.UI.Slider;

namespace _Source.Coordinators
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private Image loadingScreen;
        [SerializeField] private GameObject loadingIcon;
        [SerializeField] private List<Sprite> loadingScreenSprites;

        private int sID = 0;
        private String sName = String.Empty;
        
        public IEnumerator LoadingScreenAsync(int sceneIndex)
        {
            sID = sceneIndex;
            
            // background loading
            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(sceneIndex);
            // stop next scene from activating
            loadingOperation.allowSceneActivation = false;

            // Show LoadingImage
            loadingScreen.sprite = loadingScreenSprites[Random.Range(0, loadingScreenSprites.Count)];
            
            loadingScreen.enabled = true;
            
            // Show loadingBar
            loadingIcon.gameObject.SetActive(true);
            
            while (!loadingOperation.isDone)
            {
                if (loadingOperation.progress >= .9f)
                {
                    // allow next scene to load
                    loadingOperation.allowSceneActivation = true;
                }
                yield return null;
            }

            sID = 0;
        }
        
        public IEnumerator LoadingScreenAsync(string sceneName)
        {
            sName = sceneName;

            // background loading
            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(sceneName);
            // stop next scene from activating
            loadingOperation.allowSceneActivation = false;

            var count = loadingScreenSprites.Count;
            // Show LoadingImage
            loadingScreen.sprite = loadingScreenSprites[Random.Range(0, count)];
            
            loadingScreen.enabled = true;
            // Show loadingBar
            loadingIcon.gameObject.SetActive(true);
            
            while (!loadingOperation.isDone)
            {
                if (loadingOperation.progress >= .9f)
                {
                    // allow next scene to load
                    loadingOperation.allowSceneActivation = true;
                }
                yield return null;
            }
            
            sName = String.Empty;
        }
    
    }
}