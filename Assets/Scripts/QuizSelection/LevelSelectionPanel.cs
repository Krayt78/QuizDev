using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionPanel : MonoBehaviour
{
    public delegate void BeforeButtonPressed();
    public event BeforeButtonPressed OnBeforeButtonPressed;
    
    public delegate void NextButtonPressed();
    public event NextButtonPressed OnNextButtonPressed;
    
    public bool IsFirstLevelPanel;
    public bool IsLastLevelPanel;
    
    public Button BeforeButton;
    public Button NextButton;
    
    // Start is called before the first frame update
    void Start()
    {
        if(IsFirstLevelPanel)
        {
            BeforeButton.gameObject.SetActive(false);
        }
        else if(IsLastLevelPanel)
        {
            NextButton.gameObject.SetActive(false);
        }
        
        BeforeButton.onClick.AddListener(() => OnBeforeButtonPressed?.Invoke());
        NextButton.onClick.AddListener(() => OnNextButtonPressed?.Invoke());
    }
}
