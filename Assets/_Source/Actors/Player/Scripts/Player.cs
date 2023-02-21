using _Source.Actors.Player.Weapons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Source.Actors.Player.Scripts
{
    public class Player : MonoBehaviour
    {
        #region Private
        [SerializeField] private bool canLoot;
        [SerializeField] private Health playerHealth;
        [SerializeField] private Movement playerMovement;
        [SerializeField] private Inventory inventory;
        [SerializeField] [Tooltip("Combat AudioSource")]private AudioSource audioSource;
        private int sceneCounter = 0;
        #endregion

        #region Properties
        public static GameObject PlayerInstance { get; private set; }
        public Health Health => playerHealth;
        public Inventory Inventory => GetComponent<Inventory>();

        public int SceneCounter => sceneCounter;

        public bool CanLoot
        {
            get => canLoot;
            set => canLoot = value;
        }

        public bool InventoryIsFull => inventory.IsFull;
        #endregion
        
        private void Awake()
        {
            PlayerInstance = gameObject;
            canLoot = false;

            playerHealth.PlayerDespawning += OnPlayerDeath;
            DontDestroyOnLoad();
        }

        private void OnPlayerDeath()
        {
            playerMovement.StopControls = true;
        }

        private void DontDestroyOnLoad()
        {
            var numOfGameInstances = FindObjectsOfType<Player>();
            if (numOfGameInstances.Length > 1)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(this);
        }

        public void DeleteObject()
        {
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        }

        public void AddToSceneCounter()
        {
            sceneCounter++;
        }

        public void ToggleAudio()
        {
            if(audioSource.isPlaying)
                audioSource.Pause();
            else
                audioSource.Play();
        }

        public void SetHealthBar(bool state)
        {
            playerHealth.SetHealthBar(state);
        }
    }
}