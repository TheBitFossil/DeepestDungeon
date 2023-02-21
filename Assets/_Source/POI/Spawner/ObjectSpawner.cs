using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

namespace _Source.Coordinators.GameStates
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> objects = new List<GameObject>();

        private int rng;
        
        private void Start()
        {
            ShowRngObject();
        }
        
        private void ShowRngObject()
        {
            rng = Random.Range(0, objects.Count - 1);
            
            foreach (var go in objects)
            {
                go.SetActive(false);
            }
            
            objects[rng].SetActive(true);
        }

    }
}