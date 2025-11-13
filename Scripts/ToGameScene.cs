using UnityEngine;
using UnityEngine.SceneManagement;

public class ToGameScene : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
