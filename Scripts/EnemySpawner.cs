using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;

    public int enemyCount = 0;
    public int waveNumber = 0;
    public bool isSpawning = false;
    private GameObject[] enemies;

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0 && !isSpawning)
        {
            button.interactable = true;
            button.colors = ColorBlock.defaultColorBlock;
            buttonText.color = new Color(0f, 0f, 0f, 255f);
        }
    }






    IEnumerator EnemySpawn()
    {
        while (enemyCount < waveNumber * 5)
        {
            isSpawning = true;  
            button.interactable = false;
            button.colors = new ColorBlock
            {
                normalColor = new Color(0f, 0f, 0f, 0f)
            };
            buttonText.color = new Color(0f, 0f, 0f, 0f);
            Instantiate(enemyPrefab, startPoint.position, Quaternion.identity);
            enemyCount++;
            yield return new WaitForSeconds(spawnInterval);
        }
        isSpawning = false;
    }

    public void IncreaseWave()
    {
        waveNumber++;
        enemyCount = 0; 
        StartCoroutine(EnemySpawn());
    }


}
    


