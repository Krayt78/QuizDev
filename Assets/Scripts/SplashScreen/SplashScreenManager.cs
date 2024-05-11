using UnityEngine;
using UnityEngine.UI;

public class SplashScreenManager : MonoBehaviour
{
    public delegate void SplashScreenButtonClicked();
    public event SplashScreenButtonClicked OnSplashScreenButtonClicked;
        
    private Button _splashScreenButton;

    private void Start()
    {
        _splashScreenButton.onClick.AddListener(_OnSplashScreenButtonClicked);
    }

    private void _OnSplashScreenButtonClicked()
    {
        OnSplashScreenButtonClicked?.Invoke();
    }
}