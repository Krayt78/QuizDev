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
    
    public void SetText(string text)
    {
        AnswerText.text = text;
    }
}
