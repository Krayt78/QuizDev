using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SplashScreenManager : MonoBehaviour
{
    public delegate void SplashScreenButtonClicked();
    public event SplashScreenButtonClicked OnSplashScreenButtonClicked;
        
    public Button splashScreenButton;

    private void Start()
    {
        splashScreenButton.onClick.AddListener(_OnSplashScreenButtonClicked);
    }

    private void _OnSplashScreenButtonClicked()
    {
        OnSplashScreenButtonClicked?.Invoke();
    }
}