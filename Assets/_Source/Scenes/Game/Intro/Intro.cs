using _Source.Coordinators;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace _Source.Scenes.Game.Intro
{
    public class Intro : MonoBehaviour
    {
        [SerializeField] private VideoPlayer videoPlayer;
    
        private void Start()
        {
            GameObject camera = GameObject.Find("MainCamera");
            InputReader.AnyKeyEvent += OnSkipIntro;
        
            videoPlayer.playOnAwake = false;
            videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;

            videoPlayer.isLooping = true;

            videoPlayer.loopPointReached += EndReached;
        
            videoPlayer.Play();
        }

        private void OnSkipIntro()
        {
            LoadMainMenu();        
        }

        private void OnDestroy()
        {
            videoPlayer.loopPointReached -= EndReached;
            InputReader.AnyKeyEvent -= OnSkipIntro;
        }

        private void EndReached(VideoPlayer source)
        {
            source.Stop();
            LoadMainMenu();
        }

        private void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}