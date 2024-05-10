using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CanvasManager canvasManager;

    // Start is called before the first frame update
    void Start()
    {
        canvasManager.OnHintButtonClicked += OnHintButtonClicked;
    }

    private void OnHintButtonClicked()
    {
        Debug.Log("GameManager: Hint Button Clicked");
    }
    
    
}
