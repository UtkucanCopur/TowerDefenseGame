using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI coinText;

    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;

    [SerializeField] private GameObject enemySpawner;
    [SerializeField] private Image healthBar;


    private void Update()
    {
        if (healthBar.rectTransform.localScale.x <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
        


        
    }

    public void AddCoins(int amount)
    {
        int currentCoins = int.Parse(coinText.text);
        currentCoins += amount;
        coinText.text = currentCoins.ToString();
    }


    

    

}
