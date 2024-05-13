using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    public Image AnswerButtonColorImage;
    public Button AnswerButtonComponent;
    public TMP_Text AnswerText;
    
    public void SetColor(Color color)
    {
        
        AnswerButtonColorImage.color = color;
    }

    public void ToggleAplha()
    {
        if (!AnswerButtonComponent.interactable)
        {
            Debug.Log("AnswerButton: Button is not interactable");
            Color color = AnswerButtonColorImage.color;
            color.a = 0.75f;
            AnswerButtonColorImage.color = color;
        }
        else
        {
            Color color = AnswerButtonColorImage.color;
            color.a = 1;
            AnswerButtonColorImage.color = color;
        }
    }
    public void SetText(string text)
    {
        AnswerText.text = text;
    }
}
