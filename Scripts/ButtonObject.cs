using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonObject : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;

    public void OnClicked()
    {
        button.colors = new ColorBlock
        {
            normalColor = new Color(0f,0f, 0f, 0f)
        };

        buttonText.color = new Color(0f, 0f, 0f, 0f);
    }

}
