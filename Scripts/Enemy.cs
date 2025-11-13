using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float speed = 1f;
    private int pathIndex = 1;
    private float health = 100f;

    private int coinValue = 10;
    private GameObject gameManager;
    private Image healthBar;
    private GameObject spawner;


    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();
        spawner = GameObject.FindGameObjectWithTag("EnemySpawner");
        health += spawner.GetComponent<EnemySpawner>().waveNumber * 30;
    }

    void Update()
    {
        
        transform.position = Vector3.MoveTowards(
            transform.position, 
            PathManager.pathPoints[pathIndex].position, 
            speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, PathManager.pathPoints[pathIndex].position) < 0.1f)
        {
            
            if (pathIndex < PathManager.pathPoints.Count - 1)
            {
                pathIndex++;
                
            }
            else
            {
                Destroy(gameObject);
                healthBar.rectTransform.localScale = new Vector3(
                    healthBar.rectTransform.localScale.x - 0.1f, 
                    healthBar.rectTransform.localScale.y, 
                    healthBar.rectTransform.localScale.z);
                Debug.Log(healthBar.rectTransform.localScale);
                if (healthBar.rectTransform.localScale.x <= 0)
                {
                    SceneManager.LoadScene("MainMenu");
                }
            }
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            gameManager.GetComponent<GameManager>().AddCoins(coinValue);
        }
    }






}
