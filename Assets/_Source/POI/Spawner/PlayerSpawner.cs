using System;
using _Source.Actors.Player.Scripts;
using UnityEngine;

namespace _Source.POI.Spawner
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private Player playerPrefab;
        private Player player;

        public Player SpawnPlayer()
        {
            return Instantiate(playerPrefab, transform.position, transform.rotation);
        }

        public void SetPositionOf(Player p)
        {
            p.transform.position = transform.position;
        }
    }
}