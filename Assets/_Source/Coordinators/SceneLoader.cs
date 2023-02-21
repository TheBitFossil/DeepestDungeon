using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Source.Coordinators
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private int sceneIndexToLoad = -1;
        [SerializeField] private PlayableDirector director;
        [SerializeField] private PlayableAsset fadeOut;
        [SerializeField] private PlayableAsset fadeIn;
        [SerializeField] private LoadingScreen loadingScreen;
        
        private bool isSceneLoaded = false;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Confined;
            director.Play(fadeIn);
        }

        public void LoadSceneByIndex(int index)
        {
            StartCoroutine(loadingScreen.LoadingScreenAsync(index));
        }
    
        public void LoadSceneBuildIndexFromTimeLine(int sceneBuildIndex)
        {
            if (isSceneLoaded == false)
            {
                isSceneLoaded = true;
                sceneIndexToLoad = sceneBuildIndex;

                if (director != null)
                    director.Play(fadeOut);
            }
        }
    
        public void OnTimeLineFadeOutLoadSceneBuildIndex()
        {
            if (sceneIndexToLoad > -1)
            {
                StartCoroutine(loadingScreen.LoadingScreenAsync(sceneIndexToLoad));
            }
        }

        public void OnTimeLineFadeIn()
        {
            //Debug.Log("Scene:"+ SceneManager.GetActiveScene().name+ " is loaded!");
        }

        public void LoadMainMenu()
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Confined;
            StartCoroutine(loadingScreen.LoadingScreenAsync("MainMenu"));
        }

        public void LoadOptionsMenu()
        {
            SceneManager.LoadScene("OptionsMenu");
        }
        
        
        public void QuitApplication()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    
    }
}