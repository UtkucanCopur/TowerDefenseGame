using System;
using System.Collections.Generic;
using UnityEngine;

public class Tower1Spawner : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private Transform spawnPoint;

    private GameObject currentTower;

    private void Start()
    {
        SpawnNewTower();
    }

    public void SpawnNewTower()
    {
        currentTower = Instantiate(towerPrefab, spawnPoint.position, Quaternion.identity);
        currentTower.GetComponent<Tower1>().spawner = this; // Tower1'e spawner referansý geç
        currentTower.GetComponent<Tower1>().placeableAreaList = new List<GameObject>(GameObject.FindGameObjectsWithTag("PlaceableArea"));
    }

    
}
