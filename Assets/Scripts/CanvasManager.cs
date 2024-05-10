using UnityEngine;
using UnityEngine.UI;


public class CanvasManager : MonoBehaviour
{
    public delegate void HintButtonClicked();
    public event HintButtonClicked OnHintButtonClicked;

    public Button HintButton;

    // Start is called before the first frame update
    void Start()
    {
        HintButton.onClick.AddListener(_OnHintButtonClicked);
    }

    private void _OnHintButtonClicked()
    {
        OnHintButtonClicked?.Invoke();
    }
}
