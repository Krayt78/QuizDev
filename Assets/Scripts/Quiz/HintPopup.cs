using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HintPopup : MonoBehaviour, IDeselectHandler
{
    public TMP_Text hintText;
    
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
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
    
    public void OnDeselect(BaseEventData eventData)
    {
        HideHint();
    }
}
