using UnityEngine;

public class Tower2Spawner : MonoBehaviour
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
        currentTower.GetComponent<Tower2>().spawner = this; // Tower2'ye spawner referansý geç
        currentTower.GetComponent<Tower2>().placeableAreaList = new System.Collections.Generic.List<GameObject>(GameObject.FindGameObjectsWithTag("PlaceableArea"));
    }
}
