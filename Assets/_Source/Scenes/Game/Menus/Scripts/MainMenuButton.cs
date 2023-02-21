using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Source.Scenes.Game.Menus.Scripts
{
    public class MainMenuButton : MonoBehaviour
    {
        public void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}