using System.Collections.Generic;
using Quiz;
using UnityEngine;


public class CanvasManager : MonoBehaviour
{
    public delegate void SplashScreenButtonClicked();
    public event SplashScreenButtonClicked OnSplashScreenButtonClicked;
    
    public delegate void HintButtonClicked();
    public event HintButtonClicked OnHintButtonClicked;

    public delegate void AnswerButtonClicked(int answerIndex);
    public event AnswerButtonClicked OnAnswerButtonClicked;
    
    public delegate void QuizSelected(int quizIndex);
    public event QuizSelected OnQuizSelected;
    
    public delegate void BackButtonClicked();
    public event BackButtonClicked OnBackButtonClicked;
    
    [Header("Canvases")]
    public Canvas SplashScreenCanvas;
    public Canvas QuizSelectionCanvas;
    public Canvas QuizCanvas;
    
    private SplashScreenCanvasManager _splashScreenCanvasManager;
    private QuizCanvasManager _quizCanvasManager;
    private QuizSelectionCanvasManager _quizSelectionCanvasManager;

    public void Initialize(List<QuizScriptableObject> firstLevelQuizes, List<QuizScriptableObject> secondLevelQuizes,
        List<QuizScriptableObject> thirdLevelQuizes)
    {
        _splashScreenCanvasManager = FindObjectOfType<SplashScreenCanvasManager>();
        _splashScreenCanvasManager.OnSplashScreenButtonClicked += () => OnSplashScreenButtonClicked?.Invoke();
        
        _quizSelectionCanvasManager = FindObjectOfType<QuizSelectionCanvasManager>();
        _quizSelectionCanvasManager.Initialize(firstLevelQuizes,secondLevelQuizes, thirdLevelQuizes);
        _quizSelectionCanvasManager.OnQuizSelected += _OnQuizSelected;
        
        _quizCanvasManager = FindObjectOfType<QuizCanvasManager>();
        _quizCanvasManager.OnAnswerButtonClicked += _OnAnswerButtonClicked;
        _quizCanvasManager.OnHintButtonClicked += _OnHintButtonClicked;
        _quizCanvasManager.OnBackButtonClicked += _OnBackButtonClicked;
        _quizCanvasManager.Initialize(firstLevelQuizes[0], true);
    }
    
    private void _OnBackButtonClicked()
    {
        OnBackButtonClicked?.Invoke();
    }
    
    private void _OnQuizSelected(int quizIndex)
    {
        OnQuizSelected?.Invoke(quizIndex);
    }

    private void _OnAnswerButtonClicked(int answerIndex)
    {
        OnAnswerButtonClicked?.Invoke(answerIndex);
    }

    private void _OnHintButtonClicked()
    {
        OnHintButtonClicked?.Invoke();
    }

    public void HandleBackgrounds(int backgroundIndex)
    {
        _quizCanvasManager.HandleBackgrounds(backgroundIndex);
    }
}
