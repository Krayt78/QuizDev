using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HintPopup : MonoBehaviour
{
    public Button escapeButton;
    public TMP_Text hintText;
        
    // Start is called before the first frame update
    void Start()
    {
        escapeButton.onClick.AddListener(() => gameObject.SetActive(false));
    }
    
    public void SetHintText(string text)
    {
        hintText.text = text;
    }
    
    public void HideHint()
    {
        gameObject.SetActive(false);
    }

    public void ShowHint(string hint)
    {
        SetHintText(hint);
        gameObject.SetActive(true);
    }
}
