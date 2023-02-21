using System;
using System.Collections;
using _Source.Actors.Enemies;
using _Source.Actors.Enemies.Draugrs.Scripts;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Draugr prefab;
    [SerializeField] private GameObject enemiesContainer;
    
    public Draugr Spawn()
    {
        var d = Instantiate(prefab, transform.position, transform.rotation);
        d.transform.parent = enemiesContainer.transform;
        return d;
    }
}