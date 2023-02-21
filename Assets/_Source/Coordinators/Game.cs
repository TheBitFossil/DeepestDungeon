using System;
using _Source.Actors.Enemies.Draugrs.Scripts;
using _Source.Actors.Player.Scripts;
using _Source.Loot;
using _Source.POI.Spawner;
using _Source.Scenes.Game.Menus.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace _Source.Coordinators
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private float levelLoadDelay = 5f;
        [SerializeField] private PauseMenu pauseMenu;
        [SerializeField] private LevelEnd levelEnd;
        [SerializeField] private PlayerSpawner playerSpawn;
        [SerializeField] private EnemySpawner[] enemySpawners;
        [SerializeField] private TextMeshProUGUI levelDisplay;
        [SerializeField] private LoadingScreen loadingScreen;
        [SerializeField] private GameObject tutorialObject;
        [SerializeField] private GameObject gameOver;
        [SerializeField] private GameOverScreen gameOverScreen;
        
        private Player player;
        private UIController uiController;
        
        public PauseMenu PauseMenu => pauseMenu;

        private void Awake()
        {
            uiController = GetComponent<UIController>();
            InputReader.ToggleInventoryEvent  += OnInventoryToggled;
            InputReader.TogglePauseMenuEvent  += OnPauseMenuToggled;
            pauseMenu.PauseMenuResumeGame     += OnPauseButtonResume;
            pauseMenu.PauseMenuLoadMainMenu   += OnPauseButtonMainMenu;
            pauseMenu.PauseMenuExitApp        += OnPauseButtonExit;
        }

        private void Start()
        {
            InitObjects();
            InitPlayer();
            SetPlayerToStartLocation();
            IsPlayerAllowedToLoot(false);
            EnemySpawning();
            CheckEnemyCount();
            ActivateLevelEnd(false);
            ShowSceneCounter();
        }

        private void ShowSceneCounter()
        {
            levelDisplay.text = player.SceneCounter.ToString();
        }

        private void InitObjects()
        {
            levelEnd.PlayerReachedLevelEnd += OnPlayerReachedEndOfLevel;
        }

        private void Update()
        {
            if (!player.Health.IsAlive)
            {
                OnPlayerDeath();
            }

            AllowInputReaderAttacks();
            CheckEnemyCount();
        }

        private void AllowInputReaderAttacks()
        {
            if (uiController.IsPauseMenuActive || uiController.IsInventoryActive)
            {
                InputReader.CanAttack = false;
            }
            else
            {
                InputReader.CanAttack = true;
            }
        }

        #region Events

        private void OnDestroy()
        {
            levelEnd.PlayerReachedLevelEnd   -= OnPlayerReachedEndOfLevel;
            player.Health.PlayerDespawned    -= OnPlayerDeath;
            InputReader.ToggleInventoryEvent -= OnInventoryToggled;
            InputReader.TogglePauseMenuEvent -= OnPauseMenuToggled;
        }

        private void OnPauseButtonResume()
        {
            GameTime(1f);
            player.ToggleAudio();
        }
        
        private void OnPauseButtonMainMenu()
        {
           // Destroy Player
           player.DeleteObject();
           // Load Main Menu
           GameTime(1f);
           Cursor.lockState = CursorLockMode.Confined;
           SceneManager.LoadScene("MainMenu");
        }
        
        private void OnPauseButtonExit()
        {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
        }
        
        private void OnPlayerReachedEndOfLevel()
        {
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            player.AddToSceneCounter();
            var rng = Random.Range(4, 13);

            TryHideTutorialObject();
            SetPlayerHealthBarUI(false);
            
            StartCoroutine(loadingScreen.LoadingScreenAsync(rng));
        }


        private void TryHideTutorialObject()
        {
            if(tutorialObject == null) return;
            
            tutorialObject.SetActive(false);
        }

        private void OnInventoryToggled()
        {
            // PauseMenu is not active, while we want to open inventory
            if (false == uiController.IsPauseMenuActive)
            {
                uiController.ToggleInventory();
                
                if (uiController.IsInventoryActive)
                {
                    GameTime(0.01f);
                }
                else
                {
                    GameTime(1f);
                }
            }
        }

        private void OnPauseMenuToggled()
        {
            if (uiController.IsInventoryActive)
            {
                uiController.ToggleInventory();
            }

            uiController.TogglePauseMenu();
            player.ToggleAudio();

            if (uiController.IsPauseMenuActive)
            {
                GameTime(0f);
            }
            else
            {
                GameTime(1f);
            }
        }

        private static void GameTime(float time)
        {
            Time.timeScale = time;
        }

        public void OnPlayerDeath()
        {
            gameOver.SetActive(true);
            gameOverScreen.PlayAudio();
            Invoke(nameof(PlayerLostHealth), levelLoadDelay);
        }
        
        #endregion

        private void CheckEnemyCount()
        {
            var childCount = GetComponentsInChildren<Draugr>().Length;
            
            if (childCount <= 0)
            {
                ActivateLevelEnd(true);
                IsPlayerAllowedToLoot(true);
                ActivateLootChests();
                OpenLevelEndGate();
            }
            else
            {
                ActivateLevelEnd(false);
                IsPlayerAllowedToLoot(false);
            }
        }

        private void ActivateLootChests()
        {
            var chests = FindObjectsOfType<LootBag>();
            foreach (var lootBag in chests)
            {
                lootBag.Activate();
            }
        }
        
        private void InitPlayer()
        {
            if (!FindObjectOfType<Player>())
                player = playerSpawn.SpawnPlayer();
            else 
                player = FindObjectOfType<Player>();
            
            player.Health.PlayerDespawned += OnPlayerDeath;
            uiController.Inventory = player.GetComponent<Actors.Player.Scripts.Inventory>();
           
        }

        private void SetPlayerToStartLocation()
        {
            playerSpawn.SetPositionOf(player);
            SetPlayerHealthBarUI(true);
        }

        private void SetPlayerHealthBarUI(bool state)
        {
            player.SetHealthBar(state);
        }

        private void IsPlayerAllowedToLoot(bool state)
        {
            player.CanLoot = state;
        }
        
        private void EnemySpawning()
        {
            foreach (var enemySpawner in enemySpawners) 
            {
                enemySpawner.Spawn();
            }
            
            CheckEnemyCount();
        }

        private void ActivateLevelEnd(bool state)
        {
            levelEnd.IsActive = state;
        }

        private void OpenLevelEndGate()
        {
            levelEnd.OpenGate();
        }
        
        private void PlayerLostHealth()
        {
            if (player != null)
            {
                player.DeleteObject();
                StopRemainingEnemiesAttack();
            }
            SceneManager.LoadScene("MainMenu");
        }

        private void StopRemainingEnemiesAttack()
        {
            var remaining = GetComponentsInChildren<Draugr>();
            foreach (var enemy in remaining)
            {
                enemy.StopAttacking();
            }
        }
    }
}